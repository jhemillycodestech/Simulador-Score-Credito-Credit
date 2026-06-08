var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFront", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
    
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("PermitirFront");
//app é API rodando MapPost cria rota HTTP POST

//HTTP usa vários MÉTODOS e eu escolhi POST para o usuário enviar os dados 
//para o calculo do score.



app.UseDefaultFiles();
app.UseStaticFiles();

app.MapPost("/api/simular", (DadosEntrada Dados) =>
{
    int scoreFinal = 300;

    if(Dados.Salario >=2000)
    {
        scoreFinal += 250;
    }
    else
    {
        scoreFinal += 100;
    }

    if(Dados.TemContasEmDia)
    {
        scoreFinal += 300;
    }
   
    if(Dados.TemNomeLimpo)
    {
        scoreFinal += 150;
    }
    
    scoreFinal = Math.Clamp(scoreFinal, 0,1000);

    string classificacao = scoreFinal switch
    {
        >=701 => "Excelente",
        >=501 => "Bom",
        >=301 => "Baixo",
        _ => "Muito Baixo",
    };

    return Results.Ok(new
    {
        score = scoreFinal,
        status = classificacao,
        
    });

});


app.Run();

public record DadosEntrada(string Nome, double Salario, bool TemContasEmDia, bool TemNomeLimpo);

