using ConsoleDump;

var cotas = CotaParlamentar.LerCotasParlamentares("cota_parlamentar.csv");

//Total de gastos por partido
var totalGastoPorPartido = cotas.GroupBy(c => c.Partido) 
                               .Select(g => new { Partido = g.Key, TotalGasto = g.Sum(c => c.ValorLiquido) }) 
                               .OrderByDescending(g => g.TotalGasto) 
                               .ToArray(); 
totalGastoPorPartido.Dump(); 

//Total de gastos por deputado
var deputados = cotas.GroupBy(c => c.NomeParlamentar)
                    .Select(g => new { Nome = g.Key, TotalGasto = g.Sum(c => c.ValorLiquido) })
                    .OrderByDescending(g => g.TotalGasto)
                    .ToArray();
deputados.Dump();

//Média de gastos por mês a media de cada mes
var mediaGastosPorMes = cotas.GroupBy(c => Convert.ToInt32(c.Mes))
                             .Select(g => new { Mes = g.Key, MediaGasto = g.Average(c => c.ValorLiquido) })
                             .OrderByDescending(g => g.MediaGasto)
                             .ToArray();
mediaGastosPorMes.Dump();

//Alimentação por deputado
var alimentacaopordepu = cotas.Where(c => c.Descricao.Contains("ALIMENTAÇÃO"))
                               .GroupBy(c => c.NomeParlamentar)
                               .Select(g => new { Deputado = g.Key, TotalGasto = g.Sum(c => c.ValorLiquido) })
                               .OrderByDescending(g => g.TotalGasto)
                               .ToArray();
alimentacaopordepu.Dump();

//Forncedores mais utilizados
var fornecedoresMaisUtilizados = cotas.GroupBy(c => c.Fornecedor)
                                     .Select(g => new { Fornecedor = g.Key, Total = g.Count() })
                                     .OrderByDescending(g => g.Total)
                                     .ToArray();
fornecedoresMaisUtilizados.Dump();

//Gasto total por UF
var gastoTotalPorUF = cotas.GroupBy(c => c.UF)
                           .Select(g => new { UF = g.Key, TotalGasto = g.Sum(c => c.ValorLiquido) })
                           .OrderByDescending(g => g.TotalGasto)
                           .ToArray();
gastoTotalPorUF.Dump();


// Meses com maior número de documentos emitidos
var mesesComMaiorNumeroDeDocumentos = cotas.GroupBy(c => c.Mes)
                                            .Select(g => new { Mes = g.Key, Total = g.Count(c => c.DocumentoId.HasValue && !string.IsNullOrEmpty(c.DocumentoId.ToString())) })
                                            .OrderByDescending(g => g.Total)
                                            .ToArray();
mesesComMaiorNumeroDeDocumentos.Dump();

// Deputados com despesas acima de R$ 10.000,00
var deputadosgastoes = cotas.GroupBy(c => c.NomeParlamentar)
                    .Select(g => new { Nome = g.Key, TotalGasto = g.Sum(c => c.ValorLiquido) })
                    .Where(g => g.TotalGasto > 10000)
                    .OrderByDescending(g => g.TotalGasto)
                    .ToArray();
deputadosgastoes.Dump();

// Total gasto por tipo de despesa (Descricao)
var totalGastoPorTipoDespesa = cotas.GroupBy(c => c.Descricao)
                                    .Select(g => new { Descricao = g.Key, TotalGasto = g.Sum(c => c.ValorLiquido) })
                                    .OrderByDescending(g => g.TotalGasto)
                                    .ToArray();
totalGastoPorTipoDespesa.Dump();

//Total gasto por ano
var totalGastoAno = cotas.GroupBy(c => c.Ano)
                         .Select(g => new { Ano = g.Key, TotalGasto = g.Sum(c => c.ValorLiquido) })
                         .OrderByDescending(g => g.Ano)
                         .ToArray();
totalGastoAno.Dump();
