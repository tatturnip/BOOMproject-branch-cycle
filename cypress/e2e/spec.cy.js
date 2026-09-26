describe('Products list', () => {
  it('displays the products list', () => {
    cy.visit('http://localhost:5173')

    cy.get('h1').should('contain.text', 'Products:')

    cy.get('ul[name="products-list"]')
  })
})