        const formulario = document.getElementById('formScore');
        
        formulario.addEventListener('submit', async function(evento) {
            evento.preventDefault();

            const nomeDigitado = document.getElementById('campoNome').value;
            const salarioDigitado = parseFloat(document.getElementById('campoSalario').value);
            
            
            const valorContas = document.querySelector('input[name="contasEmDia"]:checked').value;
            const valorNomeLimpo = document.querySelector('input[name="nomeLimpo"]:checked').value;

       
            const contasEmDiaBooleano = (valorContas === "sim");
            const nomeLimpoBooleano = (valorNomeLimpo === "sim");

            
            const pacoteDados = {
                nome: nomeDigitado,
                salario: salarioDigitado,
                temContasEmDia: contasEmDiaBooleano,
                temNomeLimpo: nomeLimpoBooleano
            };

    
            const urlApi = 'http://localhost:5044/api/simular'; 

            try {
                const resposta = await fetch(urlApi, {
                    method: 'POST', 
                    headers: { 'Content-Type': 'application/json' }, 
                    body: JSON.stringify(pacoteDados) 
                });

                if (resposta.ok) {
                    const resultadoCsharp = await resposta.json();

                    document.getElementById('textoScore').innerText = resultadoCsharp.score;
                    document.getElementById('textoClassificacao').innerText = resultadoCsharp.status;

                    document.getElementById('blocoResultado').style.display = 'block';
                } else {
                    alert('Erro no processamento do servidor.');
                }

            } catch (erro) {
                console.error('A API está desligada?', erro);
                alert('Não foi possível conectar ao servidor C#. Certifique-se de que o projeto no Visual Studio está rodando!');
            }
        });
    
        function start(){
            
        }