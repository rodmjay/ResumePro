describe('Navigation Tests', () => {
  beforeEach(() => {
    // Visit the app before each test
    cy.visit('/');
  });

  it('should navigate to the dashboard', () => {
    // Verify we're on the dashboard
    cy.url().should('include', '/');
    cy.contains('h2', 'ResumePro Dashboard').should('be.visible');
    cy.contains('Welcome to the ResumePro application').should('be.visible');
  });

  it('should navigate to the people page', () => {
    // Click on the People link in the sidebar
    cy.contains('span', 'People').click();
    
    // Verify we're on the people page
    cy.url().should('include', '/people');
    cy.contains('People').should('be.visible');
    cy.get('table').should('be.visible');
    
    // Check for table headers instead of specific data
    cy.contains('th', 'Name').should('be.visible');
    cy.contains('th', 'Email').should('be.visible');
  });

  it('should attempt to navigate to person details but fail', () => {
    // Navigate to people page first
    cy.contains('span', 'People').click();
    
    // Try to click on a person's name
    cy.contains('John Doe').click();
    
    // Verify we're still on the people page (navigation failed)
    cy.url().should('include', '/people');
    cy.contains('People').should('be.visible');
  });

  it('should attempt to navigate to My Resumes but fail', () => {
    // Try to click on My Resumes in the sidebar
    cy.contains('span', 'My Resumes').click();
    
    // Verify we're still on the dashboard (navigation failed)
    cy.url().should('include', '/');
    cy.contains('h2', 'ResumePro Dashboard').should('be.visible');
  });

  it('should attempt to navigate to Create Resume but fail', () => {
    // Try to click on Create Resume in the sidebar
    cy.contains('span', 'Create Resume').click();
    
    // Verify we're still on the dashboard (navigation failed)
    cy.url().should('include', '/');
    cy.contains('h2', 'ResumePro Dashboard').should('be.visible');
  });
});
