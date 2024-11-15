    document.addEventListener('DOMContentLoaded', function () {
            const productList = document.getElementById('productList');
    const productCards = Array.from(document.querySelectorAll('.product-card'));

    // Função de ordenação
    function sortProducts(compareFn) {
                // Ordena os cartões usando a função de comparação
                const sortedProducts = productCards.sort(compareFn);
    // Limpa a lista e reanexa os produtos ordenados
    productList.innerHTML = '';
                sortedProducts.forEach(card => productList.appendChild(card));
            }

            // Funções de comparação
            const compareByPriceAsc = (a, b) => parseFloat(a.dataset.price) - parseFloat(b.dataset.price);
            const compareByPriceDesc = (a, b) => parseFloat(b.dataset.price) - parseFloat(a.dataset.price);
            const compareByNameAsc = (a, b) => a.dataset.name.localeCompare(b.dataset.name);
            const compareByNameDesc = (a, b) => b.dataset.name.localeCompare(a.dataset.name);

            // Eventos de clique para ordenação
            document.getElementById('sortPriceAsc').addEventListener('click', () => sortProducts(compareByPriceAsc));
            document.getElementById('sortPriceDesc').addEventListener('click', () => sortProducts(compareByPriceDesc));
            document.getElementById('sortNameAsc').addEventListener('click', () => sortProducts(compareByNameAsc));
            document.getElementById('sortNameDesc').addEventListener('click', () => sortProducts(compareByNameDesc));
        });