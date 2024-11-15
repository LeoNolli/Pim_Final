document.addEventListener('DOMContentLoaded', function () {
    // Verifica se a mensagem de erro está presente
    const errorDiv = document.getElementById("error");

    if (errorDiv) {
        // Define um tempo de exibição (5 segundos)
        setTimeout(() => {
            // Adiciona classe para efeito de desaparecimento
            errorDiv.style.transition = "opacity 1s";
            errorDiv.style.opacity = "0";

            // Remove o elemento da tela após o tempo definido
            setTimeout(() => errorDiv.remove(), 1000);
        }, 5000);
    }
});
