const productsList = document.querySelector('ul[name="products-list"]');

async function getProducts() {
    const response = await fetch('http://localhost:5269/productAPI');
    const products = await response.json();

    productsList.innerHTML = '';

    products.forEach(product => {
        const listItem = document.createElement('li');

        listItem.textContent =
            `${product.name} - $${product.price} - Inventory: ${product.inventory}`;

        productsList.appendChild(listItem);
    });
}

getProducts();