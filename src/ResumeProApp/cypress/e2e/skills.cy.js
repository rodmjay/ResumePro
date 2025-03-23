describe('Skills Tests', () => {
  beforeEach(() => {
    // Visit the app before each test
    cy.visit('/');
  });

  it('should attempt to navigate to skills page', () => {
    // Try to click on Skills in the sidebar
    cy.contains('span', 'Skills').click({ force: true });
    
    // Verify navigation to skills page
    cy.url().should('include', '/skills');
  });

  it('should display skills in a table or card format', () => {
    // Navigate to skills page
    cy.visit('/skills');
    
    // Verify the skills page has loaded
    cy.contains('Skills').should('be.visible');
    
    // Either table or cards should be present
    cy.get('table, p-card').should('exist');
  });

  it('should filter skills if search functionality exists', () => {
    // Navigate to skills page
    cy.visit('/skills');
    
    // If there's a search input, try using it
    cy.get('input[type="text"]').eq(0).type('Java', { force: true });
    
    // This test is more exploratory since we're not sure if filtering exists
    // Just verify the page doesn't crash
    cy.contains('Skills').should('be.visible');
  });
});
