# Harness de validação numérica

Compara os quatro métodos de resolução de `frmCalculo.cs` (Birchall, Runge-Kutta 5,
Runge-Kutta 45, Adams-Moulton) contra soluções analíticas fechadas de modelos
compartimentais simples, para confirmar que os defeitos corrigidos na Fase 1
(`C-4`, `G-5`, `C-3`, `G-7` — ver `DOCUMENTACAO.md` §9) produzem resultados
numericamente corretos, e não apenas "compila".

Não toca no banco de dados nem em nenhum `Control`: constrói os modelos em memória
com a API pública de `Ipen.CompartimentalModel`, e invoca os métodos privados de
`frmCalculo` (`Calculo`, `Init`, `MontarEquacao`, `ResolverPorKutta5/45/AdamsM`) via
reflection sobre uma instância criada com `FormatterServices.GetUninitializedObject`
— o construtor nunca roda, então `InitializeComponent()` nunca cria nenhum `Control`.
Isso só é seguro porque os métodos exercitados tocam apenas campos numéricos privados
da classe, nenhum deles referencia a UI.

## Casos de teste

- **Teste A** — decaimento radioativo puro, 1 compartimento sem ligações:
  `x(t) = e^(−λt)`.
- **Teste B** — transferência entre 2 compartimentos, sem decaimento:
  `x1(t) = e^(−kt)`, `x2(t) = 1 − e^(−kt)`.
- **Teste C** — decaimento **e** duas vias de eliminação independentes a partir do
  mesmo compartimento. É o cenário exato do defeito **C-4**: antes da correção,
  `QuantAnt` era uma única variável compartilhada entre todos os compartimentos de
  eliminação, e o incremento reportado para o segundo compartimento usava o valor
  anterior do primeiro. Este teste reproduz literalmente a lógica de incremento por
  compartimento (hoje inline em `btnCalcular_Click`/`SolveRungeKutta`, e por isso
  não isolável em um método chamável) e compara contra a diferença analítica
  `x(t) − x(t−Δt)`.

## Tolerâncias

Birchall resolve a exponencial de matriz por série de Taylor com tolerância relativa
`1e-10` (`terr`) — é essencialmente exato, e bate a solução analítica com erro entre
`1e-11` e `3e-8` em todos os casos. RK5/RK45/Adams-Moulton são integradores numéricos
reais de passo fixo (1–2 dias nestes testes): têm erro de discretização genuíno, por
isso a tolerância é mais frouxa (`3e-3` nos valores brutos, `5e-2` nos incrementos,
onde a diferença entre dois valores já aproximados amplifica o erro relativo mesmo
quando o erro absoluto é minúsculo).

## Como rodar

```bash
msbuild ..\..\CBT.sln /t:Rebuild /p:Configuration=Release
dotnet build Validate.csproj -c Release
dotnet run --project Validate.csproj -c Release --no-build
```

Sai com código 0 se todos os casos passarem na tolerância, 1 caso contrário, e
imprime uma tabela linha a linha com valor calculado, valor analítico e erro relativo
para cada verificação.

## Último resultado (2026-07-27)

474 verificações, 0 falhas, pior erro relativo entre as aprovadas: `2.97e-2`
(um incremento do Adams-Moulton no Teste C, erro absoluto `2.6e-5`).
