# IPEN — Sistema de Modelagem Compartimental (CBT / SSID)

> Documentação técnica gerada a partir da análise completa do código-fonte.
> Referência: commit `b31c4a4` (2026-07-27). Repositório: `https://github.com/tclaro/ipen`.

---

## Sumário

1. [Visão geral](#1-visão-geral)
2. [Estrutura da solução](#2-estrutura-da-solução)
3. [Modelo de domínio — `Ipen.CompartimentalModel`](#3-modelo-de-domínio--ipencompartimentalmodel)
4. [Editor gráfico — `Ipen.CBT.UI`](#4-editor-gráfico--ipencbtui)
5. [Motor de cálculo — `Ipen.SSID.UI`](#5-motor-de-cálculo--ipenssidui)
6. [Fundamentos matemáticos](#6-fundamentos-matemáticos)
7. [Persistência de dados](#7-persistência-de-dados)
8. [Fluxos de uso](#8-fluxos-de-uso)
9. [Análise crítica: defeitos e riscos](#9-análise-crítica-defeitos-e-riscos)
10. [Dívida técnica e arquitetura](#10-dívida-técnica-e-arquitetura)
11. [Build, execução e implantação](#11-build-execução-e-implantação)
12. [Recomendações priorizadas](#12-recomendações-priorizadas)

---

## 1. Visão geral

### 1.1 Propósito

O sistema modela e resolve **modelos compartimentais biocinéticos** aplicados à
dosimetria interna de radionuclídeos — a área de proteção radiológica que estima
quanta atividade de um material radioativo permanece em cada órgão/tecido do corpo
ao longo do tempo após uma incorporação (inalação, ingestão etc.).

Um modelo compartimental representa o corpo como um grafo dirigido:

- **Compartimentos** (nós) — órgãos, tecidos ou regiões (Pulmão, Sangue, Fígado, Osso,
  Excreção Urinária…). Cada um contém uma fração da atividade incorporada.
- **Ligações** (arestas) — taxas de transferência de primeira ordem `k(i,j)` em dia⁻¹,
  descrevendo o fluxo de material de um compartimento para outro.

A evolução do sistema é regida por um sistema de EDOs lineares acopladas, resolvido
numericamente para produzir curvas de retenção/excreção ao longo do tempo.

### 1.2 Os dois aplicativos

| Aplicativo | Assembly | Papel |
|---|---|---|
| **CBT** — *Compartimental Box Tool* | `CBT.exe` | Editor visual. Desenha o modelo (caixas e setas), define nomes, cores, taxas de transferência, frações de incorporação. Persiste em Access ou exporta XML. |
| **SSID** | `Ipen.SSID.UI.exe` | Solver. Carrega um modelo (Access ou XML), resolve o sistema de EDOs por um de quatro métodos numéricos, gera relatório HTML e gráfico log-log. |

Ambos compartilham a biblioteca de domínio `Ipen.CompartimentalModel.dll`.

### 1.3 Linha do tempo

O código nasceu em **2007** e teve desenvolvimento ativo até **2011** (75 commits,
autor principal `T.Claro`). Os 15 anos seguintes foram de dormência, com o commit
de 2026 reativando o projeto para adicionar suporte a **incorporação fracionada**.

---

## 2. Estrutura da solução

```
ipen-master/
├── CBT.sln                        # solução completa (3 projetos) — VS 2010 → atualizada p/ VS 18
├── SSID.sln                       # solução do solver (2 projetos) — VS 2010
├── CompartimentalModel.sln        # solução só da biblioteca — VC# Express 2008
│
├── Ipen.CompartimentalModel/      # [Library] Domínio + persistência — .NET 3.5
│   ├── Caixas.cs                  #   compartimento (Control do WinForms)
│   ├── CaixasCollection.cs        #   coleção de compartimentos + agregação de eventos
│   ├── Linhas.cs                  #   ligação/transferência entre dois compartimentos
│   ├── LinhasCollection.cs        #   coleção de ligações
│   ├── Sistema.cs                 #   agregado raiz (SINGLETON) — caixas + linhas
│   ├── Modelos.cs                 #   metadados do modelo + Sistema
│   ├── TipoModelos.cs             #   tipo do modelo (lookup)
│   ├── Reservatorio.cs            #   DataSet tipado — mapeamento objeto ↔ XML
│   ├── DataXML.cs                 #   fachada de import/export XML
│   ├── DataBD.cs                  #   camada de acesso ao Access (OleDb)
│   ├── Configuracoes.cs           #   estado global (conn string + flags de exibição)
│   └── DrawingUtils.cs            #   utilitários GDI+ (HSB→RGB, retângulo arredondado)
│
├── Ipen.CBT.UI/                   # [WinExe] Editor gráfico — .NET 3.5
│   ├── frmPrincipal.cs            #   1217 linhas — janela principal, faz quase tudo
│   ├── Painel.cs                  #   canvas customizado: desenha linhas, setas, rótulos
│   ├── CaixaProp.cs               #   diálogo de propriedades do compartimento
│   ├── LinhaProp.cs               #   diálogo de propriedades da ligação
│   ├── frmEditModelo.cs           #   editor tabular (duplica frmPrincipal)
│   ├── frmModelos.cs              #   seletor de modelos do banco
│   ├── frmGrafico.cs              #   ⚠ ÓRFÃO — não compilado, não compila
│   ├── Starter.cs                 #   ⚠ ÓRFÃO — 2º entry point, não compilado
│   └── CBT.csproj                 #   ⚠ projeto antigo, obsoleto (o ativo é Ipen.CBT.UI.csproj)
│
├── Ipen.SSID.UI/                  # [WinExe] Solver — .NET 3.5
│   ├── frmCalculo.cs              #   925 linhas — UI + 4 solvers + geração de relatório
│   ├── frmGrafico.cs              #   janela de gráfico destacável
│   ├── frmModelos.cs              #   seletor de modelos do banco
│   ├── Compartimento.cs           #   ⚠ DTO legado, sem uso efetivo
│   ├── Conexao.cs                 #   ⚠ conexão legada, substituída por Configuracoes
│   ├── DotNumerics.dll            #   biblioteca de métodos numéricos (Runge-Kutta, Adams)
│   └── ZedGraph.dll               #   biblioteca de gráficos v5.1.5
│
└── Database/
    ├── Modelos.mdb                # banco Access (Jet 4.0) — repositório de modelos
    ├── Iodo 131 Fast.xml          # modelo exemplo, schema v3 (único formato suportado)
    └── uranio234-s.html           # relatório HTML exportado (amostra)
```

### 2.1 Grafo de dependências

```
Ipen.CBT.UI ──┐
              ├──> Ipen.CompartimentalModel ──> System.Data (OleDb), System.Drawing,
Ipen.SSID.UI ─┘                                 System.Windows.Forms, System.Xml
     │
     ├──> DotNumerics.dll   (EDOs: Runge-Kutta 5, Runge-Kutta 45, Adams-Moulton)
     ├──> ZedGraph.dll      (gráficos científicos)
     └──> System.Web        (apenas para HttpUtility.HtmlEncode)
```

> **Observação arquitetural.** `Ipen.CompartimentalModel` referencia
> `System.Windows.Forms` porque `Caixas` **herda de `Control`**. O modelo de domínio
> está fundido com a camada de apresentação — ver [§10.1](#101-o-domínio-é-a-ui).

---

## 3. Modelo de domínio — `Ipen.CompartimentalModel`

### 3.1 `Caixas` — o compartimento

[`Caixas.cs`](Ipen.CompartimentalModel/Caixas.cs) — herda de `System.Windows.Forms.Control`.
É simultaneamente a entidade de domínio **e** o controle visual arrastável na tela.

**Propriedades de domínio:**

| Propriedade | Tipo | Significado |
|---|---|---|
| `Numero` | `int` | Índice 1..N, reatribuído automaticamente a cada inserção/remoção |
| `Nome` | `string` | Rótulo do órgão/tecido |
| `Acompanhar` | `bool` | Se marcado, o compartimento aparece no relatório e no gráfico |
| `Eliminacao` | `bool` | Compartimento de excreção: reporta **incremento** no intervalo, não acúmulo |
| `Incorporacao` | `bool` | Recebe atividade no instante t=0 (condição inicial) |
| `Fracao` | `double` | Fração da incorporação depositada aqui em t=0 (Σ deveria ser 1,0) |

**Propriedades visuais:** `BackColor` (cor do gradiente), `ForeColor` (calculada
automaticamente por contraste HSB — [`Caixas.cs:90`](Ipen.CompartimentalModel/Caixas.cs:90)),
`Size` (auto-ajustado ao texto), `PontoCentral`, `PontosExtremos`.

**Renderização** ([`Caixas.cs:323-400`](Ipen.CompartimentalModel/Caixas.cs:323)) —
desenho custom em back-buffer, invalidado a cada mudança de propriedade:
retângulo arredondado com gradiente vertical, realce de luz superior semitransparente,
número no canto e nome centralizado com sombra. Compartimentos de **incorporação
aparecem sublinhados** ([`Caixas.cs:375`](Ipen.CompartimentalModel/Caixas.cs:375)).

**Interação:** arraste com o mouse (`OnMouseDown`/`OnMouseMove`/`OnMouseUp`), com
clamp em `Left/Top >= 0`.

**Eventos customizados:** `Moved`, `Deleted`, `PropertyChanged` — todos via
`BoxEventArgs`, propagados coleção acima.

### 3.2 `Linhas` — a transferência

[`Linhas.cs`](Ipen.CompartimentalModel/Linhas.cs) — POCO puro (não é `Control`).
Referencia duas `Caixas` e carrega até duas taxas.

| Propriedade | Tipo | Significado |
|---|---|---|
| `CaixaInicio` / `CaixaFim` | `Caixas` | Extremidades da ligação |
| `DirecaoDaLinha` | `enum Direcao` | `InicioParaFim=1`, `FimParaInicio=2`, `Ambos=3` |
| `ValorAB` | `float` | Taxa k(início→fim), em dia⁻¹ |
| `ValorBA` | `float` | Taxa k(fim→início), em dia⁻¹ |

As propriedades `ValorAB`/`ValorBA` **zeram-se automaticamente** quando a direção não
as comporta ([`Linhas.cs:234-265`](Ipen.CompartimentalModel/Linhas.cs:234)) — invariante
imposta no getter *e* no setter.

**Geometria:** `CoeficienteAngular`, `PontoCentral`, `PontoTercoInicio`/`PontoTercoFim`
(posicionamento dos dois rótulos em ligações bidirecionais), `XdeY(y)`/`YdeX(x)`
(interseção da reta com as bordas da caixa, usada para desenhar as setas),
`PontoNessaLinha(pto)` (hit-test com tolerância de ±5 px).

**Nomenclatura:** `SugerirNome()` gera rótulos no formato `K1,2 = 1.50e-02`.

### 3.3 `Sistema` — o agregado raiz

[`Sistema.cs`](Ipen.CompartimentalModel/Sistema.cs) — contém `CaixasCollection` e
`LinhasCollection`, e reencaminha todos os eventos das caixas para os assinantes da UI.

> ⚠️ **`Sistema` é um singleton** ([`Sistema.cs:35-43`](Ipen.CompartimentalModel/Sistema.cs:35)).
> Toda instância de `Modelos` recebe a **mesma** coleção. Consequência prática: o
> aplicativo só consegue ter **um modelo aberto por vez**, e dois objetos `Modelos`
> distintos compartilham silenciosamente caixas e linhas. Ver [§10.2](#102-o-singleton-sistema).

**Métodos:** `ObterLinhaPorCaixas(cx1, cx2)` (busca bidirecional),
`ContarLigacoesAssociadas(cx)`, `Clear()`.

**Cascata de exclusão:** ao excluir uma caixa, `_caixas_BoxDeleted`
([`Sistema.cs:122`](Ipen.CompartimentalModel/Sistema.cs:122)) remove automaticamente
todas as linhas incidentes.

### 3.4 Coleções

**`CaixasCollection : List<Caixas>`** — no `Add` conecta 10 handlers de evento na caixa
e chama `Reindex()`, que renumera todas as caixas para 1..N.

> ⚠️ Os métodos `Add`/`Remove`/`RemoveAt` usam `new` (ocultação), **não** `override` —
> `List<T>` não os declara virtuais. Se a coleção for acessada por uma referência do
> tipo `List<Caixas>`, a fiação de eventos e a reindexação são **silenciosamente
> ignoradas**.

**`LinhasCollection : List<Linhas>`** — acrescenta a sobrecarga `Remove(Caixas cx)`,
que remove todas as ligações incidentes à caixa (iteração reversa, correta).

### 3.5 `Modelos` e `TipoModelos`

`Modelos` agrega os metadados persistidos:

| Campo | Tipo | Uso |
|---|---|---|
| `idModelo` | `int` | PK no Access; `0` = ainda não gravado |
| `nmModelo` | `string` | Nome |
| `dtCriacao` | `DateTime` | Data de criação |
| `Descricao` | `string` | Texto livre |
| `meiaVida` | `double` | Meia-vida física do radionuclídeo, **em dias** |
| `Tipo` | `TipoModelos` | Classificação (Fast/Medium/Slow — tabela `TipoModelo`) |
| `Colecao` | `Sistema` | Caixas + linhas (singleton) |

A meia-vida alimenta a constante de decaimento λ = ln(2)/T½, aplicada uniformemente a
todos os compartimentos.

---

## 4. Editor gráfico — `Ipen.CBT.UI`

### 4.1 `frmPrincipal` — a janela principal

1217 linhas. Concentra edição, persistência, configuração e apresentação.

**Layout:** `SplitContainer` — painel superior com abas (*Compartimentos* / *Ligações*),
painel inferior com o `Painel` (canvas de desenho).

**Aba Compartimentos:** nome, cor, e os quatro flags (`Acompanhar`, `Eliminacao`,
`Incorporacao`, `Fracao`). O campo Fração só é habilitado quando *Incorporação* está
marcada ([`frmPrincipal.cs:1007`](Ipen.CBT.UI/frmPrincipal.cs:1007)).

**Aba Ligações:** dois combos (origem/destino) + valor de transferência. A lógica de
`btnAddLig_Click` ([`frmPrincipal.cs:690-759`](Ipen.CBT.UI/frmPrincipal.cs:690)) trata
o caso de ligação já existente: se no mesmo sentido, pergunta se deseja alterar; se em
sentido oposto, **promove automaticamente a ligação para bidirecional** (`Direcao.Ambos`)
e grava o valor em `ValorBA`. É a parte mais sutil do editor.

**Menu Exibição:** três flags globais persistidos em `app.config` — exibir rótulos,
exibir setas de direção, exibir apenas ligações do compartimento selecionado.

**Máquina de estados de criação de ligação** ([`frmPrincipal.cs:461-510`](Ipen.CBT.UI/frmPrincipal.cs:461)):
`Normal → SolicitandoLinhaA → SolicitandoLinhaB → Normal`, com o cursor mudando para cruz.

### 4.2 `Painel` — o canvas

[`Painel.cs`](Ipen.CBT.UI/Painel.cs) — `Panel` customizado. As **caixas são controles
filhos** (o próprio WinForms as desenha e trata o arraste); as **linhas são desenhadas
no `OnPaint` do painel**, por baixo dos controles.

Responsabilidades:

- **Desenho de ligações** — espessura 3 e rótulo destacado quando alguma extremidade
  está selecionada; rótulo único ao centro para ligações unidirecionais, dois rótulos
  nos terços para bidirecionais.
- **Setas de direção** ([`Painel.cs:266-319`](Ipen.CBT.UI/Painel.cs:266)) — calcula em
  qual das quatro bordas da caixa a reta incide e desenha um triângulo apontando para
  dentro.
- **Anti-sobreposição** (`VerificarCaixasSobrepostas`) — desloca a caixa 5 px à direita
  em laço até não colidir com nenhuma outra.

### 4.3 Formulários auxiliares

- **`CaixaProp`** — diálogo modal de propriedades do compartimento.
- **`LinhaProp`** — diálogo da ligação, com botão que cicla `-->` / `<--` / `<-->` e
  sugestão automática de nome.
- **`frmModelos`** — grid dos modelos gravados no Access, com abrir/excluir.
- **`frmEditModelo`** — editor tabular; **duplica ~80% de `frmPrincipal`**.

---

## 5. Motor de cálculo — `Ipen.SSID.UI`

### 5.1 `frmCalculo`

925 linhas. UI, quatro solvers, montagem da matriz e geração de relatório no mesmo arquivo.

**Métodos numéricos disponíveis** (menu, mutuamente exclusivos):

| Método | Implementação | Origem |
|---|---|---|
| **Birchall** | Manual, [`frmCalculo.cs:256-419`](Ipen.SSID.UI/frmCalculo.cs:256) | Código próprio |
| **Runge-Kutta 5** (implícito) | `OdeImplicitRungeKutta5` | DotNumerics |
| **Runge-Kutta 45** (explícito, passo adaptativo) | `OdeExplicitRungeKutta45` | DotNumerics |
| **Adams-Moulton** | `OdeAdamsMoulton` | DotNumerics |

**Entradas:** tempo final e passo (campos de texto), meia-vida (vem do modelo).

**Saídas:**
- Tabela HTML renderizada em `WebBrowser`, com uma coluna por compartimento marcado
  `Acompanhar` + coluna *Total*; exportável para `.html`.
- Gráfico ZedGraph em escala **log-log** (`Tempo (dias)` × `Quantidade (Fração da
  Incorporação)`), opcionalmente em janela destacada.
- Tempo de processamento decorrido.

### 5.2 A matriz `R`

Existem **duas montagens incompatíveis** da matriz, escolhidas conforme o método:

**`PreencherMatrizR()`** — para Birchall ([`frmCalculo.cs:438`](Ipen.SSID.UI/frmCalculo.cs:438)):
- Dimensão `(N+1) × (N+1)`; **índice 0 não é usado** (compartimentos são 1..N).
- `R[i][j]` (i≠j) = taxa de transferência de i para j.
- `R[i][i]` = **fração inicial** do compartimento i — a diagonal é reaproveitada para
  armazenar a condição inicial, que depois é extraída em `xo[i] = R[i,i]`
  ([`frmCalculo.cs:278`](Ipen.SSID.UI/frmCalculo.cs:278)).

**`PreencherMatrizR(bool)`** — para os métodos DotNumerics ([`frmCalculo.cs:470`](Ipen.SSID.UI/frmCalculo.cs:470)):
- Dimensão `N × N`; índices **base 0**.
- `R[i][i]` = −(soma de todas as taxas de saída de i) — diagonal é a taxa de perda.
- `R[i][j]` = taxa de entrada em i vinda de j.
- É a matriz da EDO propriamente dita, consumida por `MontarEquacao`.

> ⚠️ As duas convenções diferem em **dimensão, base de índice e semântica da diagonal**.
> Alternar de método sem recarregar o modelo é fonte de bugs — ver [§9.2](#92-graves).

### 5.3 `MontarEquacao` — o sistema de EDOs

[`frmCalculo.cs:801-817`](Ipen.SSID.UI/frmCalculo.cs:801) — callback entregue ao DotNumerics:

```
dyᵢ/dt = −λ·yᵢ + Σⱼ R[i][j]·yⱼ
```

onde λ = 0,693/T½ é o decaimento radioativo, aplicado a todos os compartimentos.

### 5.4 O algoritmo de Birchall

Implementação da exponencial de matriz por **série de Taylor com scaling-and-squaring**:

1. **Montagem de A** ([`frmCalculo.cs:259-281`](Ipen.SSID.UI/frmCalculo.cs:259)) —
   `a[i][j] = R[j][i]` (transposta), e `a[i][i] = −λ − Σₖ≠ᵢ R[i][k]` (perdas).
2. **Escalonamento** — `A ← A·t`, depois divide por 2^iz até que `‖A‖ < 0,2`, com
   `iz` escolhido pelo laço em [`frmCalculo.cs:297`](Ipen.SSID.UI/frmCalculo.cs:297).
3. **Série de Taylor** — `sum = I + A + A²/2! + A³/3! + …`, com no máximo 10 000 termos
   e tolerância relativa `terr = 1e-10`.
4. **Squaring** — eleva o resultado ao quadrado `iz` vezes para desfazer o escalonamento.
5. **Solução** — `xt = exp(A·t) · xo`.
6. **Termo de fonte** — se λ ≠ 0, `Inversao()` calcula A⁻¹ por Gauss-Jordan e monta o
   vetor `u` de material acumulado por decaimento.

> A cada passo de tempo, `Calculo()` recalcula **toda** a exponencial de matriz a partir
> de t = 0. Não há propagação incremental. Custo: O(passos × n³ × iterações).

---

## 6. Fundamentos matemáticos

### 6.1 O modelo

Para N compartimentos com quantidade `qᵢ(t)`:

```
dqᵢ/dt = Σⱼ≠ᵢ kⱼᵢ·qⱼ  −  qᵢ·(λ + Σⱼ≠ᵢ kᵢⱼ)
         └─ entradas ─┘     └── decaimento + saídas ──┘
```

Em forma matricial: **q̇ = A·q**, com `q(0) = f` (vetor de frações de incorporação).

### 6.2 Solução

A solução analítica é `q(t) = e^{At}·q(0)`. O sistema oferece duas famílias de solução:

- **Birchall** — calcula `e^{At}` diretamente (exato até a tolerância da série).
  Vantagem: passo arbitrariamente grande sem perda de estabilidade. Adequado a
  sistemas *stiff*, que são a regra em biocinética (taxas que diferem em ordens de
  grandeza — minutos vs. décadas).
- **Runge-Kutta / Adams-Moulton** — integração passo a passo. RK45 é explícito
  (rápido, mas instável em sistemas stiff); RK5 implícito e Adams-Moulton lidam
  melhor com stiffness.

### 6.3 Convenções de unidade

| Grandeza | Unidade |
|---|---|
| Tempo | dias |
| Taxas de transferência `k` | dia⁻¹ |
| Meia-vida | dias |
| Quantidade | fração adimensional da incorporação (Σ frações iniciais = 1,0) |

### 6.4 Compartimentos de eliminação

Compartimentos marcados `Eliminacao` (urina, fezes) são **acumuladores monotônicos**.
Para eles, o relatório apresenta o **incremento no intervalo** (`q(t) − q(t−Δt)`), que é
a grandeza fisicamente mensurável em bioensaio — e não o acumulado
([`frmCalculo.cs:135-139`](Ipen.SSID.UI/frmCalculo.cs:135)).

---

## 7. Persistência de dados

### 7.1 Access (`Modelos.mdb`) — repositório principal

Provider: `Microsoft.Jet.OLEDB.4.0`. Caminho configurado em `app.config`
(chave `MDBPath`) e mantido em `Configuracoes.Arquivo`.

**Esquema (4 tabelas):**

```
TipoModelo
  idTipoModelo  (PK)
  nmTipoModelo

Modelo
  idModelo      (PK, autonumeração)
  nmModelo, dtCriacao, dtAlteracao, Descricao
  idTipoModelo  (FK → TipoModelo)
  meiaVida

TableCaixas                          TableLinhas
  idModelo (FK → Modelo, cascata)      idModelo (FK → Modelo, cascata)
  Numero, Nome                         CaixaInicio, CaixaFim   (→ TableCaixas.Numero)
  Left, Top, Width, Height             CorR, CorG, CorB
  CorR, CorG, CorB                     Direcao
  Acompanhar, Eliminacao               ValorAB, ValorBA
  Incorporacao, Fracao
```

**Estratégia de gravação** ([`DataBD.cs`](Ipen.CompartimentalModel/DataBD.cs)):
`GravarModelo` decide entre INSERT (`idModelo == 0`) e UPDATE. No UPDATE, apaga todas
as caixas do modelo (linhas caem por cascata declarada no Access) e reinsere tudo —
**replace-all, sem diff**.

### 7.2 XML — formato de intercâmbio

`DataXML` + `Reservatorio` (um `DataSet` com as três tabelas) fazem serialização via
`WriteXml`/`ReadXml` com `XmlWriteMode.WriteSchema` — o XSD viaja embutido no arquivo.

Historicamente circularam **três versões incompatíveis de schema**:

| Versão | Tabela `Modelo` | `TipoModelo` | `Incorporacao` / `Fracao` | Situação |
|---|---|---|---|---|
| **v1** | ❌ ausente | — | ❌ ausentes | 🗑 exemplos removidos do repositório |
| **v2** | ✅ | `xs:string`, sem valor nos dados | ✅ / `xs:double` | 🗑 exemplos removidos do repositório |
| **v3** | ✅ | `xs:int` | ✅ / `xs:string` | ✅ formato corrente |

`Reservatorio.ImportarArquivo` suporta **apenas o v3**. Tolera a ausência das colunas
`Incorporacao`/`Fracao` ([`Reservatorio.cs:117-131`](Ipen.CompartimentalModel/Reservatorio.cs:117)),
mas **não** tolera a ausência da tabela `Modelo` nem `TipoModelo` nulo.

Os arquivos de exemplo v1 (`Database/Uranium.xml`) e v2 (`Ipen.CBT.UI/uranio234-s.xml`)
foram **removidos do repositório**, por estarem defasados e não abrirem. Resta
`Database/Iodo 131 Fast.xml` como único exemplo. Se ainda existirem arquivos v1/v2 em
uso fora do repositório, será preciso migrá-los ou endurecer o importador — ver
[§9.1 C-2](#91-críticos).

**Nota sobre `Fracao` como string.** A mudança de 2026 passou a gravar `Fracao` como
texto com `CultureInfo.InvariantCulture` e formato `"0.####################"`
([`Reservatorio.cs:81`](Ipen.CompartimentalModel/Reservatorio.cs:81)). O motivo é
evitar notação científica e perda de dígitos na serialização de `double`, e blindar
contra a vírgula decimal do pt-BR. A leitura aceita ambos os tipos
([`Reservatorio.cs:126-131`](Ipen.CompartimentalModel/Reservatorio.cs:126)) — decisão
correta para compatibilidade.

### 7.3 Configuração (`app.config`)

| Chave | Uso | Aplicativos |
|---|---|---|
| `MDBPath` | Caminho do banco Access | CBT, SSID |
| `XMLPath` | Último diretório de XML usado | CBT, SSID |
| `Rotulos`, `Setas`, `Ligacoes` | Flags de exibição do canvas | CBT |

`GravarSettings` escreve no `.exe.config` **ao lado do executável** — exige permissão
de escrita no diretório de instalação.

---

## 8. Fluxos de uso

### 8.1 Criar e resolver um modelo

```
CBT.exe
  └─ Ferramentas → Configurar banco de dados → seleciona Modelos.mdb
  └─ Arquivo → Novo
  └─ Aba Compartimentos: para cada órgão
       nome, cor, [Acompanhar], [Eliminação], [Incorporação + Fração]
  └─ Aba Ligações: origem → destino, valor k (dia⁻¹)
  └─ preenche Nome, Descrição, Tipo, Meia-vida
  └─ Arquivo → Salvar         (grava no Access)
     ou Arquivo → Exportar    (gera XML)

SSID.exe
  └─ Carregar MDB (escolhe o modelo) ou Carregar XML
  └─ Método → Birchall | Runge-Kutta 5 | Runge-Kutta 45 | Adams-Moulton
  └─ informa Tempo final e Passo (dias)
  └─ Calcular
  └─ aba Relatório (HTML) + aba Gráfico (log-log)
  └─ Salvar HTML
```

### 8.2 Fluxo interno de um cálculo (Birchall)

```
btnCalcular_Click
  ├─ PreencherMatrizR()      # R[i][i] ← Fracao ; R[i][j] ← taxas ; Tag ← 1..N
  ├─ Init()                  # aloca sum, a, term, b, xt, xo, u, q, qi
  └─ para T = 0 .. Final+1:
       ├─ Calculo()          # A ← Rᵀ com diagonal de perdas
       │    ├─ escalona A·t / 2^iz
       │    ├─ série de Taylor → sum ≈ e^(A·t/2^iz)
       │    ├─ squaring iz vezes → sum ≈ e^(A·t)
       │    ├─ Inversao() se λ≠0 → A⁻¹ (Gauss-Jordan)
       │    └─ xt ← sum · xo
       ├─ acumula linha da tabela HTML
       ├─ alimenta PointPairList do ZedGraph
       └─ Tempo += Passo
```

---

## 9. Análise crítica: defeitos e riscos

Achados classificados por severidade, com referência a arquivo e linha.

> **Status da Fase 1** — corrigidos e compilando: **C-1**, **C-3**, **C-4**, **G-5**, **G-7**.
> **C-2** foi mitigado pela remoção dos exemplos defasados, mas o importador continua sem
> as guardas. Os demais achados seguem abertos.

### 9.1 Críticos

---

**C-1 · Importar XML no CBT descarta todos os metadados do modelo** — ✅ CORRIGIDO

[`frmPrincipal.cs:1202-1204`](Ipen.CBT.UI/frmPrincipal.cs:1202) e
[`frmPrincipal.cs:203-205`](Ipen.CBT.UI/frmPrincipal.cs:203)

```csharp
DataXML interfaceXML = new DataXML(openFile.FileName);
interfaceXML.ImportarXML();
// ← o Modelo importado NUNCA é atribuído a this.Modelo
foreach (Caixas cx in this.Modelo.Colecao.Caixas)  // usa o objeto antigo
```

As caixas aparecem na tela **apenas por acidente**: `DataXML` cria um `Modelos` interno
cujo `Colecao` é o mesmo singleton `Sistema`. Mas `nmModelo`, `Descricao`, `meiaVida` e
`Tipo` ficam com os valores anteriores (vazios). Importar um XML e salvar em seguida
**grava um modelo sem nome, sem descrição e com meia-vida zero** — ou seja, o cálculo
subsequente ignora o decaimento radioativo.

*Correção:* `this.Modelo = interfaceXML.Modelo;` após `ImportarXML()`, seguido de
`CarregarTela()` para refletir os campos na UI.

---

**C-2 · Arquivos XML dos schemas v1 e v2 não abrem** — ⚠️ MITIGADO (exemplos removidos; importador ainda sem guardas)

[`Reservatorio.cs:108`](Ipen.CompartimentalModel/Reservatorio.cs:108) e
[`Reservatorio.cs:114`](Ipen.CompartimentalModel/Reservatorio.cs:114)

```csharp
foreach (DataRow dr in ds.Tables["Modelo"].Rows)     // v1: Tables["Modelo"] é null → NRE
    ...
    Modelo.Tipo.idTipoModelo = Convert.ToInt32(dr["TipoModelo"]);  // v2: DBNull → InvalidCastException
```

- `Database/Uranium.xml` (v1) não tem a tabela `Modelo` → **NullReferenceException**.
- `Ipen.CBT.UI/uranio234-s.xml` (v2) declara `TipoModelo` no schema mas **nenhuma linha
  tem valor** → `Convert.ToInt32(DBNull.Value)` lança **InvalidCastException**.

Ambos os arquivos estão versionados no repositório e são apresentados como exemplos.
As guardas defensivas foram adicionadas para `Incorporacao`/`Fracao` mas não para
`Modelo`/`TipoModelo`.

*Correção:*

```csharp
if (ds.Tables.Contains("Modelo"))
    foreach (DataRow dr in ds.Tables["Modelo"].Rows) {
        ...
        Modelo.Tipo.idTipoModelo = dr.IsNull("TipoModelo") ? 0 : Convert.ToInt32(dr["TipoModelo"]);
    }
```

---

**C-3 · `TodosCompartimentos` nunca é limpo no caminho dos métodos DotNumerics** — ✅ CORRIGIDO

[`frmCalculo.cs:470-481`](Ipen.SSID.UI/frmCalculo.cs:470)

`PreencherMatrizR()` (Birchall) chama `TodosCompartimentos.Clear()` na linha 446.
A sobrecarga `PreencherMatrizR(bool)` **não chama**. Cada execução de Runge-Kutta ou
Adams-Moulton **acrescenta** os compartimentos à lista já existente. Após carregar um
segundo modelo, a lista contém caixas de ambos, e `CreateChart` associa curvas aos
compartimentos errados ([`frmCalculo.cs:207`](Ipen.SSID.UI/frmCalculo.cs:207) —
`C = TodosCompartimentos[i-1]`), produzindo **gráficos com nomes e cores trocados**.

*Correção:* adicionar `TodosCompartimentos.Clear();` no início de `PreencherMatrizR(bool)`.

---

**C-4 · `QuantAnt` é compartilhado entre todos os compartimentos de eliminação** — ✅ CORRIGIDO

[`frmCalculo.cs:76`](Ipen.SSID.UI/frmCalculo.cs:76) + [`135-139`](Ipen.SSID.UI/frmCalculo.cs:135)
(Birchall) e [`frmCalculo.cs:635`](Ipen.SSID.UI/frmCalculo.cs:635) + [`728-732`](Ipen.SSID.UI/frmCalculo.cs:728) (RK/Adams)

```csharp
double QuantAnt = 0;                       // ← UMA variável para todos
foreach (Caixas C in TodosCompartimentos) {
    if (C.Eliminacao) {
        valorInstante = xt[indice] - QuantAnt;   // subtrai o valor do OUTRO compartimento
        QuantAnt = xt[indice];
    }
}
```

Com **um** compartimento de eliminação o cálculo está correto. Com **dois ou mais**
(cenário normal: urina + fezes), o incremento de cada um é calculado subtraindo o valor
anterior do compartimento *errado*. Os valores de excreção do relatório ficam
**numericamente inválidos**, inclusive negativos.

*Correção:* usar `Dictionary<Caixas,double>` ou `double[] quantAnt = new double[n]`,
indexado por compartimento.

---

### 9.2 Graves

**G-1 · `SELECT` com palavras reservadas do Jet sem colchetes**
[`DataBD.cs:286-290`](Ipen.CompartimentalModel/DataBD.cs:286)

```sql
SELECT Numero, Nome, Left, Top, Width, Height, ...
```

`Left` é **função** em Jet SQL; `Top` é palavra reservada. O `INSERT` correspondente
([`DataBD.cs:132`](Ipen.CompartimentalModel/DataBD.cs:132)) usa corretamente `[Left]`,
`[Top]`. A inconsistência indica que o caminho de leitura do Access provavelmente falha
ou depende de tolerância do provider. *Correção:* colchetes em todos os identificadores.

**G-2 · Auto-ligações produzem `CaixaFim` nulo**
[`Reservatorio.cs:141-150`](Ipen.CompartimentalModel/Reservatorio.cs:141) e
[`DataBD.cs:258-267`](Ipen.CompartimentalModel/DataBD.cs:258)

```csharp
if (cx.Numero == (int)dr["CaixaInicio"]) cxInicio = cx;
else if (cx.Numero == (int)dr["CaixaFim"]) cxFim = cx;   // ← else impede o mesmo objeto
```

Se `CaixaInicio == CaixaFim`, `cxFim` fica `null`, e `Painel.OnPaint`
([`Painel.cs:202`](Ipen.CBT.UI/Painel.cs:202)) lança NRE ao desenhar.
*Correção:* trocar `else if` por `if` independente.

**G-3 · `double.Parse` sem validação em três pontos**
[`frmPrincipal.cs:588`](Ipen.CBT.UI/frmPrincipal.cs:588),
[`frmEditModelo.cs:84`](Ipen.CBT.UI/frmEditModelo.cs:84),
[`CaixaProp.cs:237`](Ipen.CBT.UI/CaixaProp.cs:237)

Campo Fração vazio ou com texto inválido → `FormatException` não tratada → o aplicativo
fecha. Note que `float.TryParse` **é** usado corretamente para o valor de transferência
([`frmPrincipal.cs:698`](Ipen.CBT.UI/frmPrincipal.cs:698)) — a inconsistência é o problema.
Mesmo vale para `Convert.ToDouble(txtMeiaVida.Text)` em
[`frmPrincipal.cs:1070`](Ipen.CBT.UI/frmPrincipal.cs:1070).

**G-4 · NullReferenceException ao salvar sem modelo aberto**
[`frmPrincipal.cs:158`](Ipen.CBT.UI/frmPrincipal.cs:158)

`this.Modelo` só é instanciado em `FecharModelo()`, chamado por *Novo* e *Abrir*.
Acionar Salvar logo após abrir o aplicativo desreferencia `null`.

**G-5 · Teste de convergência da série de Taylor sem valor absoluto** — ✅ CORRIGIDO
[`frmCalculo.cs:327-329`](Ipen.SSID.UI/frmCalculo.cs:327)

```csharp
if (term[i, j] / sum[i, j] > terr)  goto volta;
```

Quando a razão é **negativa** (termos alternantes, comuns em matrizes com autovalores
negativos — exatamente o caso aqui), o teste passa como convergido prematuramente. A
série pode ser truncada cedo demais, **subestimando silenciosamente a exponencial**.
*Correção:* `Math.Abs(term[i,j] / sum[i,j]) > terr`.

**G-6 · Vazamento de handles GDI+ no `OnPaint`**
[`Painel.cs:222`](Ipen.CBT.UI/Painel.cs:222), [`234-247`](Ipen.CBT.UI/Painel.cs:234),
[`274-318`](Ipen.CBT.UI/Painel.cs:274)

`new Pen(...)`, `new SolidBrush(...)` e `new GraphicsPath()` são criados a cada
repintura e **nunca descartados**. Em um modelo com dezenas de ligações e repintura a
cada movimento do mouse, o consumo de handles GDI cresce continuamente até a degradação
ou o limite de 10 000 handles por processo. *Correção:* envolver em `using`, ou promover
a campos reutilizáveis.

**G-7 · Constante de decaimento divergente entre solvers** — ✅ CORRIGIDO
[`frmCalculo.cs:71`](Ipen.SSID.UI/frmCalculo.cs:71) vs
[`frmCalculo.cs:811`](Ipen.SSID.UI/frmCalculo.cs:811)

Birchall usa `Math.Log(2)` (0,6931471805599453); `MontarEquacao` usa o literal `0.693`.
Erro relativo de ~2×10⁻⁴ — pequeno por passo, mas **acumulativo** ao longo de milhares
de dias, tornando os métodos não comparáveis entre si.

---

### 9.3 Moderados

| # | Achado | Local |
|---|---|---|
| M-1 | `Descricao` não é HTML-encoded no relatório (`nmModelo` é). Descrição com `<`, `>` ou `&` quebra a tabela | [`frmCalculo.cs:90`](Ipen.SSID.UI/frmCalculo.cs:90), [`687`](Ipen.SSID.UI/frmCalculo.cs:687) |
| M-2 | `ConectarBancoDeDados` compara com `""` mas `LerSettings` devolve `null` para chave ausente | [`frmPrincipal.cs:51`](Ipen.CBT.UI/frmPrincipal.cs:51) |
| M-3 | `GravarSettings` lança NRE se a chave não existir no `.config` (indexador devolve `null`) | [`frmPrincipal.cs:520`](Ipen.CBT.UI/frmPrincipal.cs:520) |
| M-4 | `OleDbDataReader` nunca é fechado em `PreencherCaixas`/`PreencherLinhas` (só a conexão, via `CloseConnection`) | [`DataBD.cs:251`](Ipen.CompartimentalModel/DataBD.cs:251), [`295`](Ipen.CompartimentalModel/DataBD.cs:295) |
| M-5 | Nenhuma conexão/comando OleDb usa `using`; exceção no meio vaza a conexão do pool | `DataBD.cs` inteiro |
| M-6 | Nome de parâmetro digitado errado: `"Incoporacao"` (funciona só porque OleDb é posicional) | [`DataBD.cs:147`](Ipen.CompartimentalModel/DataBD.cs:147) |
| M-7 | `RemoverModelo` concatena `idModelo` na SQL em vez de parametrizar (baixo risco por ser `int`, mas padrão inconsistente com o resto) | [`DataBD.cs:28`](Ipen.CompartimentalModel/DataBD.cs:28) |
| M-8 | Caminho de desenvolvedor `D:\Projetos\SVN\trunk\...` versionado nos dois `app.config` | `Ipen.CBT.UI/app.config`, `Ipen.SSID.UI/App.config` |
| M-9 | `Application.Exit()` em vez de `this.Close()` no menu Sair do SSID — pula o descarte de formulários | [`frmCalculo.cs:613`](Ipen.SSID.UI/frmCalculo.cs:613) |
| M-10 | `BackBuffer` (Bitmap) não é liberado no `Dispose` da `Caixas`, só em `DestruirBuffer` | [`Caixas.cs:439`](Ipen.CompartimentalModel/Caixas.cs:439) |
| M-11 | Eixos do gráfico fixados em `AxisType.Log`; valor zero ou negativo (comum com o bug C-4) não é plotável | [`frmCalculo.cs:230-231`](Ipen.SSID.UI/frmCalculo.cs:230) |
| M-12 | `iz` chega a 1001 se o laço não convergir, dividindo por 2¹⁰⁰¹ e zerando a matriz sem aviso | [`frmCalculo.cs:297`](Ipen.SSID.UI/frmCalculo.cs:297) |
| M-13 | Nenhuma validação de que a soma das frações de incorporação seja 1,0 | — |
| M-14 | Nenhuma validação de taxa negativa ou de modelo desconexo antes de calcular | — |

---

## 10. Dívida técnica e arquitetura

### 10.1 O domínio é a UI

`Caixas` herda de `System.Windows.Forms.Control`. Consequências:

- A biblioteca de domínio **não roda sem WinForms** — inviável portar para web, serviço,
  linha de comando ou teste automatizado headless.
- Posição na tela (`Left`, `Top`, `Width`, `Height`) é persistida junto com dados
  científicos, misturando apresentação e modelo no schema do banco.
- Cada compartimento carrega o overhead de um handle de janela do Windows. Modelos com
  centenas de compartimentos esbarram no limite de controles filhos.
- Impossível instanciar um modelo em um teste unitário sem bomba de mensagens.

*Caminho de correção:* extrair um `Compartimento` POCO puro e criar um
`CompartimentoView : Control` separado que o renderiza.

### 10.2 O singleton `Sistema`

[`Sistema.cs:35-43`](Ipen.CompartimentalModel/Sistema.cs:35) — `getInstance()` sem
sincronização, atribuído no construtor de `Modelos`
([`Modelos.cs:91`](Ipen.CompartimentalModel/Modelos.cs:91)).

Efeitos observáveis:

- **Um único modelo por processo.** Não há como abrir dois modelos lado a lado nem
  comparar resultados.
- O bug C-1 fica **mascarado**: o código parece funcionar porque a coleção é global.
- Nunca é resetado entre importações — resíduo de estado entre operações.
- Não é thread-safe (irrelevante hoje, pois tudo roda na thread de UI, mas bloqueia
  qualquer paralelização do solver).

### 10.3 Duplicação de código

| Duplicação | Escopo |
|---|---|
| `frmEditModelo` × `frmPrincipal` | ~400 linhas quase idênticas (gestão de compartimentos e ligações) |
| `LerSettings` / `GravarSettings` | Reimplementados em `frmPrincipal` e `frmCalculo` |
| Geração do relatório HTML | Blocos quase idênticos em `btnCalcular_Click` e `SolveRungeKutta` |
| `Conexao.Conectar` × `Configuracoes.Conectar` | Duas conexões OleDb; a de `Conexao.cs` está morta |
| `CreateChart` | Definido em `frmCalculo` e em `frmGrafico` |

### 10.4 Código morto versionado

| Arquivo | Situação |
|---|---|
| `Ipen.CBT.UI/frmGrafico.cs` | **Não está no `.csproj`** e referencia a classe `Desktop`, que **não existe no repositório** — não compilaria |
| `Ipen.CBT.UI/Starter.cs` | Segundo entry point `Main()`, não compilado (colidiria com `Program.cs`) |
| `Ipen.CBT.UI/CBT.csproj` | Projeto obsoleto; o ativo é `Ipen.CBT.UI.csproj` |
| `Ipen.SSID.UI/Compartimento.cs` | DTO usado apenas por `LerDataSet`, que está inteiramente comentado |
| `Ipen.SSID.UI/Conexao.cs` | Substituído por `Configuracoes.Conectar` |
| `frmCalculo.LerDataSet` | 30 linhas comentadas ([`frmCalculo.cs:515-544`](Ipen.SSID.UI/frmCalculo.cs:515)) |
| ~15 handlers vazios | `mnuEditarLocalizar_Click`, `mnuFerramentasOpcoes_Click`, `novoToolStripMenuItem_Click`, etc. |

### 10.5 Estilo e manutenibilidade

- **`goto` em código numérico** — quatro ocorrências (`Fora`, `volta`, `exit`, `desvio`)
  em `Calculo()` e `Inversao()`. Reflete tradução direta de FORTRAN/BASIC. Os rótulos
  `volta:` e `desvio:` só existem para hospedar `int xxx = 0;` — um no-op.
- **Nomes de uma letra** no núcleo matemático (`n`, `a`, `b`, `q`, `qi`, `xt`, `xo`, `u`,
  `lam`, `terr`, `iz`, `ir`, `id`) sem qualquer comentário explicativo.
- **Encoding ISO-8859-1** em ~8 arquivos `.cs`, com o restante em ASCII. Sem BOM
  consistente, ferramentas modernas (Git, VS Code, revisão em web) exibem mojibake nos
  acentos. *Correção:* converter tudo para UTF-8 com BOM.
- **Estado global mutável** — `Configuracoes` expõe quatro campos `static` públicos
  (`Arquivo`, `ExibirRotulos`, `ExibirSetas`, `ExibirTodasLigacoes`).
- **Sem testes.** Nenhum projeto de teste; nenhum caso de regressão para o solver.
- **Sem README, sem `.gitignore`, sem CI.** Artefatos de build (`bin/`, `obj/`, `.vs/`)
  não estão ignorados.
- **Mensagens de erro para o usuário final** com texto de desenvolvedor:
  `"Stupid Error"` ([`frmCalculo.cs:57`](Ipen.SSID.UI/frmCalculo.cs:57)),
  `"Unexpected ERROR!!"` ([`frmPrincipal.cs:825`](Ipen.CBT.UI/frmPrincipal.cs:825)).

---

## 11. Build, execução e implantação

### 11.1 Requisitos

| Item | Versão | Observação |
|---|---|---|
| .NET Framework | **3.5** | `TargetFrameworkVersion v3.5` nos três projetos |
| Visual Studio | 2010+ | `CBT.sln` e `CompartimentalModel.sln` já foram atualizados para o formato do VS 18; `SSID.sln` continua no formato do VS 2010 |
| Microsoft Jet OLEDB | **4.0** | ⚠️ **Somente 32 bits** — ver 11.2 |
| DotNumerics | 1.0.0.0 | Incluída em `Ipen.SSID.UI/DotNumerics.dll` |
| ZedGraph | 5.1.5.28844 | Incluída em `Ipen.SSID.UI/ZedGraph.dll` |

### 11.2 ⚠️ Bloqueio de plataforma 64 bits

Os projetos têm `Platform = AnyCPU`. Em Windows 64 bits, o processo carrega como 64 bits
e **`Microsoft.Jet.OLEDB.4.0` não existe em 64 bits** — não há e nunca haverá driver.
Toda operação com o banco Access falha com `"The 'Microsoft.Jet.OLEDB.4.0' provider is
not registered on the local machine"`.

Duas saídas:

1. **Imediata** — marcar `<PlatformTarget>x86</PlatformTarget>` nos dois executáveis.
2. **Preferível** — migrar para `Microsoft.ACE.OLEDB.12.0` e converter o `.mdb` para
   `.accdb`, o que também destrava o Windows moderno.

### 11.3 Compilar

```bash
msbuild CBT.sln /p:Configuration=Release /p:PlatformTarget=x86
```

### 11.4 Configurar antes do primeiro uso

Editar `MDBPath` nos dois `app.config` — os valores versionados apontam para
`D:\Projetos\SVN\trunk\Database\Modelos.mdb`, um caminho de máquina de desenvolvedor de 2011.

O diretório de instalação precisa de **permissão de escrita**, pois `GravarSettings`
altera o `.exe.config` em tempo de execução. Instalar em `C:\Program Files\` sem elevação
faz a persistência de preferências falhar silenciosamente.

---

## 12. Recomendações priorizadas

### Fase 1 — Correções de correção científica ✅ CONCLUÍDA

Esses defeitos produzem **números errados sem sinalizar erro** — a categoria mais
perigosa em software de dosimetria.

1. **C-4** — `QuantAnt` por compartimento. Sem isso, todo modelo com mais de uma via de
   excreção reporta valores inválidos.
2. **G-5** — `Math.Abs` no teste de convergência da série de Taylor.
3. **C-3** — limpar `TodosCompartimentos` em `PreencherMatrizR(bool)`.
4. **G-7** — unificar a constante de decaimento em `Math.Log(2)`.
5. **C-1** — atribuir `this.Modelo` após importar XML (meia-vida zerada desliga o
   decaimento radioativo do cálculo).

### Fase 2 — Robustez

6. **C-2** — guardas para XML v1/v2, ou script de migração dos arquivos exemplo para v3.
7. **G-3** — `TryParse` em todos os campos numéricos.
8. **G-1** — colchetes nas palavras reservadas do Jet.
9. **G-2** — `else if` → `if` no pareamento de caixas.
10. **G-4** — instanciar `Modelo` no construtor de `frmPrincipal`.
11. **G-6** — `using` em todos os objetos GDI+ do `OnPaint`.
12. **M-4/M-5** — `using` em conexões, comandos e readers OleDb.

### Fase 3 — Higiene de repositório (baixo custo, alto retorno)

13. Adicionar `.gitignore` (`bin/`, `obj/`, `.vs/`, `*.user`, `Backup/`, `UpgradeLog.htm`).
14. Adicionar `README.md` com propósito, requisitos e instruções de build.
15. Remover código morto ([§10.4](#104-código-morto-versionado)) — `frmGrafico.cs`,
    `Starter.cs`, `CBT.csproj`, `Compartimento.cs`, `Conexao.cs`, `LerDataSet`.
16. Converter todos os `.cs` para UTF-8 com BOM.
17. Substituir o `MDBPath` versionado por caminho relativo (`.\Database\Modelos.mdb`).

### Fase 4 — Modernização

18. `PlatformTarget = x86` (imediato) ou migração para ACE.OLEDB + `.accdb` (definitivo).
19. Projeto de testes com casos analíticos conhecidos: decaimento de compartimento único
    (`q(t) = q₀·e^{−λt}`), sistema de dois compartimentos com solução fechada, e
    verificação de conservação de massa (Σ compartimentos + eliminados = 1,0).
20. Extrair `Compartimento` POCO de `Caixas : Control` — pré-requisito para os testes
    do item 19 e para qualquer portabilidade.
21. Eliminar o singleton `Sistema`; `Modelos` passa a possuir sua própria `Sistema`.
22. Unificar `frmEditModelo` e `frmPrincipal`.
23. Extrair o solver de `frmCalculo` para uma classe `Solver` sem dependência de UI —
    hoje é impossível calcular sem abrir um formulário.
24. Migrar o alvo para .NET Framework 4.8 (suportado no Windows atual) ou .NET 8 com
    Windows Forms.

---

## Apêndice A — Referência rápida de arquivos

| Arquivo | Linhas | Responsabilidade |
|---|---:|---|
| `Ipen.CBT.UI/frmPrincipal.cs` | 1217 | Janela principal do editor |
| `Ipen.SSID.UI/frmCalculo.cs` | 925 | UI do solver + 4 métodos numéricos + relatório |
| `Ipen.CBT.UI/frmGrafico.cs` | 598 | ⚠️ órfão, não compila |
| `Ipen.CBT.UI/frmEditModelo.cs` | 512 | Editor tabular (duplica frmPrincipal) |
| `Ipen.CompartimentalModel/Caixas.cs` | 450 | Compartimento (entidade + controle visual) |
| `Ipen.CBT.UI/LinhaProp.cs` | 428 | Diálogo de propriedades da ligação |
| `Ipen.CBT.UI/Painel.cs` | 321 | Canvas: linhas, setas, rótulos |
| `Ipen.CompartimentalModel/DataBD.cs` | 319 | Acesso a dados (Access/OleDb) |
| `Ipen.CBT.UI/CaixaProp.cs` | 294 | Diálogo de propriedades do compartimento |
| `Ipen.CompartimentalModel/Linhas.cs` | 291 | Ligação/transferência |
| `Ipen.CompartimentalModel/Sistema.cs` | 188 | Agregado raiz (singleton) |
| `Ipen.CompartimentalModel/CaixasCollection.cs` | 159 | Coleção + agregação de eventos |
| `Ipen.CompartimentalModel/Reservatorio.cs` | 156 | Mapeamento objeto ↔ XML |
| `Ipen.CompartimentalModel/DrawingUtils.cs` | 113 | Utilitários GDI+ |
| `Ipen.CompartimentalModel/Modelos.cs` | 112 | Metadados do modelo |
| `Ipen.CompartimentalModel/DataXML.cs` | 66 | Fachada de import/export XML |

## Apêndice B — Glossário

| Termo | Significado |
|---|---|
| **Compartimento** / *Caixa* | Órgão, tecido ou região que retém atividade |
| **Ligação** / *Linha* | Transferência de primeira ordem entre dois compartimentos |
| **k(i,j)** | Coeficiente de transferência de i para j, em dia⁻¹ |
| **Incorporação** | Entrada de material radioativo no organismo (t = 0) |
| **Fração** | Parcela da incorporação depositada em um compartimento em t = 0 |
| **Acompanhar** | Flag: compartimento incluído no relatório e no gráfico |
| **Eliminação** | Compartimento de excreção; reporta incremento, não acúmulo |
| **Meia-vida** | T½ físico do radionuclídeo, em dias; λ = ln(2)/T½ |
| **Birchall** | Método de exponencial de matriz por série de Taylor com scaling-and-squaring |
| **Stiff** | Sistema de EDOs com escalas de tempo muito díspares — típico em biocinética |
| **AMAD** | *Activity Median Aerodynamic Diameter* — parâmetro granulométrico de aerossóis |
| **Fast / Medium / Slow** | Classes de absorção pulmonar da ICRP (tabela `TipoModelo`) |

---

*Documento gerado por análise estática do código-fonte. Os defeitos relatados foram
identificados por leitura; recomenda-se validação experimental antes de correção,
especialmente para os itens numéricos da Fase 1.*
