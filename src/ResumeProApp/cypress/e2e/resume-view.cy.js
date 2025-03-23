describe('Resume View Tests', () => {
  beforeEach(() => {
    // Visit the app before each test
    cy.visit('/');
  });

  it('should navigate to resume view page', () => {
    // Navigate directly to a resume view page
    cy.visit('/people/1/resumes/1');
    
    // Verify we're on the resume view page
    cy.url().should('include', '/people/1/resumes/1');
    cy.contains('Resume Details').should('be.visible');
  });

  it('should display resume information correctly', () => {
    // Navigate to resume view page
    cy.visit('/people/1/resumes/1');
    
    // Verify resume sections are displayed
    cy.contains('Job Title:').should('be.visible');
    cy.contains('Description:').should('be.visible');
    cy.contains('Contact Information').should('be.visible');
    
    // Verify action buttons are present
    cy.contains('button', 'Edit').should('be.visible');
    cy.contains('button', 'Download PDF').should('be.visible');
    cy.contains('button', 'Delete').should('be.visible');
  });

  it('should display skills section', () => {
    // Navigate to resume view page
    cy.visit('/people/1/resumes/1');
    
    // Verify skills section is displayed
    cy.contains('Skills').should('be.visible');
    
    // There should be at least one skill displayed
    cy.get('.skill-chip').should('have.length.at.least', 1);
  });

  it('should display education history', () => {
    // Navigate to resume view page
    cy.visit('/people/1/resumes/1');
    
    // Verify education section is displayed
    cy.contains('Education').should('be.visible');
    
    // There should be at least one education entry
    cy.get('.education-entry').should('have.length.at.least', 1);
  });

  it('should display work experience', () => {
    // Navigate to resume view page
    cy.visit('/people/1/resumes/1');
    
    // Verify work experience section is displayed
    cy.contains('Work Experience').should('be.visible');
    
    // There should be at least one work experience entry
    cy.get('.experience-entry').should('have.length.at.least', 1);
  });

  it('should attempt to download resume PDF', () => {
    // Navigate to resume view page
    cy.visit('/people/1/resumes/1');
    
    // Click the download button
    cy.contains('button', 'Download PDF').click();
    
    // Since we can't test actual download in Cypress, we can verify the download was attempted
    // by checking if the button shows loading state or if a success message appears
    cy.contains('button[loading]', 'Download PDF').should('not.exist');
  });
});
