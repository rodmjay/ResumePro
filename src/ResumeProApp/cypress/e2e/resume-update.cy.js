describe('Resume Update Tests', () => {
  beforeEach(() => {
    // Visit the app before each test
    cy.visit('/');
  });

  it('should navigate to resume edit page', () => {
    // Navigate to resume view page first
    cy.visit('/people/1/resumes/1');
    
    // Click the edit button
    cy.contains('button', 'Edit').click();
    
    // Verify we're on the resume edit page
    cy.url().should('include', '/people/1/resumes/1/edit');
    cy.contains('Edit Resume').should('be.visible');
  });

  it('should display form fields with existing data', () => {
    // Navigate to resume edit page
    cy.visit('/people/1/resumes/1/edit');
    
    // Verify form fields are present and populated
    cy.get('#jobTitle').should('be.visible').should('not.have.value', '');
    cy.get('#description').should('be.visible').should('not.have.value', '');
    
    // Verify resume settings section is present
    cy.contains('Resume Settings').should('be.visible');
    
    // Verify all checkboxes are present
    cy.get('#showEmail').should('be.visible');
    cy.get('#showPhone').should('be.visible');
    cy.get('#showLinkedIn').should('be.visible');
    cy.get('#showGitHub').should('be.visible');
    cy.get('#showLocation').should('be.visible');
  });

  it('should validate required fields', () => {
    // Navigate to resume edit page
    cy.visit('/people/1/resumes/1/edit');
    
    // Clear required fields
    cy.get('#jobTitle').clear();
    cy.get('#description').clear();
    
    // Try to submit the form
    cy.contains('button', 'Save Changes').click();
    
    // Verify validation errors are displayed
    cy.contains('Job title is required').should('be.visible');
    cy.contains('Description is required').should('be.visible');
  });

  it('should update resume details successfully', () => {
    // Navigate to resume edit page
    cy.visit('/people/1/resumes/1/edit');
    
    // Update fields
    const newJobTitle = 'Updated Job Title ' + Date.now();
    cy.get('#jobTitle').clear().type(newJobTitle);
    cy.get('#description').clear().type('Updated description for testing purposes');
    
    // Toggle some settings
    cy.get('#showEmail').click();
    
    // Submit the form
    cy.contains('button', 'Save Changes').click();
    
    // Verify success message
    cy.contains('Resume updated successfully').should('be.visible');
    
    // Verify we're redirected to the view page
    cy.url().should('include', '/people/1/resumes/1');
    
    // Verify the updated information is displayed
    cy.contains(newJobTitle).should('be.visible');
    cy.contains('Updated description for testing purposes').should('be.visible');
  });

  it('should cancel edit and return to view page', () => {
    // Navigate to resume edit page
    cy.visit('/people/1/resumes/1/edit');
    
    // Click the cancel button
    cy.contains('button', 'Cancel').click();
    
    // Verify we're redirected to the view page
    cy.url().should('include', '/people/1/resumes/1');
    cy.contains('Resume Details').should('be.visible');
  });
});
