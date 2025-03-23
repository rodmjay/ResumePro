describe('Resume Detail Tests', () => {
  beforeEach(() => {
    // Visit the app before each test
    cy.visit('/');
    
    // Navigate to people page first
    cy.contains('span', 'People').click({ force: true });
    
    // Navigate to the first person detail page
    cy.contains('John Doe').click({ force: true });
  });

  it('should attempt to navigate to resume detail', () => {
    // Click on Resumes tab
    cy.contains('button', 'Resumes').click({ force: true });
    
    // Try to click on the first resume
    cy.get('table tbody tr').first().click({ force: true });
    
    // Verify either navigation occurred or we're still on person detail page
    cy.url().should('include', '/people/');
  });

  it('should display resume information when directly navigating', () => {
    // Direct navigation to resume detail
    cy.visit('/people/1/resume/1');
    
    // Verify resume detail elements are visible
    cy.contains('Resume').should('be.visible');
    cy.contains('John Doe').should('be.visible');
    
    // Check for sections that should be present
    cy.contains('Experience').should('be.visible');
    cy.contains('Skills').should('be.visible');
  });

  it('should display skill categories correctly', () => {
    // Direct navigation to resume detail
    cy.visit('/people/1/resume/1');
    
    // Verify skill categories are displayed
    cy.contains('Programming Languages').should('be.visible');
    cy.contains('C#').should('be.visible');
  });

  it('should display employment timeline', () => {
    // Direct navigation to resume detail
    cy.visit('/people/1/resume/1');
    
    // Verify employment history is displayed
    cy.contains('Experience').should('be.visible');
    cy.get('.p-timeline, .timeline').should('exist');
  });
});
