describe('Person Detail Tests', () => {
  beforeEach(() => {
    // Visit the app before each test
    cy.visit('/');
    
    // Navigate to people page first
    cy.contains('span', 'People').click({ force: true });
  });

  it('should navigate to person detail page', () => {
    // Click on a person name
    cy.contains('John Doe').click({ force: true });
    
    // Verify we're on the person detail page
    cy.url().should('include', '/people/');
    cy.contains('Personal Information').should('be.visible');
  });

  it('should display person information correctly', () => {
    // Navigate to person detail page
    cy.visit('/people/1');
    
    // Verify basic information is displayed
    cy.contains('Personal Information').should('be.visible');
    cy.contains('John Doe').should('be.visible');
    cy.contains('john.doe@example.com').should('be.visible');
  });

  it('should display tab navigation correctly', () => {
    // Navigate to person detail page
    cy.visit('/people/1');
    
    // Check that tabs exist and can be clicked
    cy.contains('button', 'Resumes').click({ force: true });
    cy.contains('h3', 'Resumes').should('be.visible');
    
    cy.contains('button', 'Education').click({ force: true });
    cy.contains('h3', 'Education').should('be.visible');
    
    cy.contains('button', 'Skills').click({ force: true });
    cy.contains('h3', 'Skills').should('be.visible');
    
    cy.contains('button', 'Languages').click({ force: true });
    cy.contains('h3', 'Languages').should('be.visible');
    
    cy.contains('button', 'Companies').click({ force: true });
    cy.contains('h3', 'Companies').should('be.visible');
  });

  it('should attempt to edit person details', () => {
    // Navigate to person detail page
    cy.visit('/people/1');
    
    // Click edit button
    cy.contains('button', 'Edit').click({ force: true });
    
    // Verify edit form appears
    cy.contains('Edit Person').should('be.visible');
    cy.get('#firstName').should('be.visible');
    cy.get('#lastName').should('be.visible');
    
    // Close the dialog without saving
    cy.get('.p-dialog-header-close').click({ force: true });
  });
});
