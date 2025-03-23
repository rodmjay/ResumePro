describe('Resume Download Tests', () => {
  beforeEach(() => {
    // Visit the app before each test
    cy.visit('/');
  });

  it('should navigate to resume download page', () => {
    // Navigate to resume view page first
    cy.visit('/people/1/resumes/1');
    
    // Click the download button
    cy.contains('button', 'Download PDF').click();
    
    // Verify download options dialog is displayed
    cy.contains('Download Options').should('be.visible');
  });

  it('should display download format options', () => {
    // Navigate to resume view page
    cy.visit('/people/1/resumes/1');
    
    // Click the download button
    cy.contains('button', 'Download PDF').click();
    
    // Verify format options are displayed
    cy.contains('PDF Format').should('be.visible');
    cy.contains('Word Format').should('be.visible');
    cy.contains('Plain Text').should('be.visible');
    
    // Verify download buttons are present
    cy.contains('button', 'Download').should('be.visible');
    cy.contains('button', 'Cancel').should('be.visible');
  });

  it('should select different template options', () => {
    // Navigate to resume view page
    cy.visit('/people/1/resumes/1');
    
    // Click the download button
    cy.contains('button', 'Download PDF').click();
    
    // Verify template options are displayed
    cy.contains('Template').should('be.visible');
    
    // Select a different template
    cy.get('#templateSelector').click();
    cy.contains('Modern').click();
    
    // Verify template preview updates
    cy.get('.template-preview').should('be.visible');
  });

  it('should attempt to download PDF format', () => {
    // Navigate to resume view page
    cy.visit('/people/1/resumes/1');
    
    // Click the download button
    cy.contains('button', 'Download PDF').click();
    
    // Select PDF format if not already selected
    cy.get('#formatPdf').check();
    
    // Click download button
    cy.contains('button', 'Download').click();
    
    // Since we can't test actual download in Cypress, we can verify the download was attempted
    // by checking if a success message appears or if the dialog closes
    cy.contains('Download Options').should('not.exist');
    cy.contains('Download started').should('be.visible');
  });

  it('should attempt to download Word format', () => {
    // Navigate to resume view page
    cy.visit('/people/1/resumes/1');
    
    // Click the download button
    cy.contains('button', 'Download PDF').click();
    
    // Select Word format
    cy.get('#formatWord').check();
    
    // Click download button
    cy.contains('button', 'Download').click();
    
    // Verify download attempt
    cy.contains('Download Options').should('not.exist');
    cy.contains('Download started').should('be.visible');
  });

  it('should attempt to download plain text format', () => {
    // Navigate to resume view page
    cy.visit('/people/1/resumes/1');
    
    // Click the download button
    cy.contains('button', 'Download PDF').click();
    
    // Select plain text format
    cy.get('#formatText').check();
    
    // Click download button
    cy.contains('button', 'Download').click();
    
    // Verify download attempt
    cy.contains('Download Options').should('not.exist');
    cy.contains('Download started').should('be.visible');
  });

  it('should cancel download', () => {
    // Navigate to resume view page
    cy.visit('/people/1/resumes/1');
    
    // Click the download button
    cy.contains('button', 'Download PDF').click();
    
    // Click cancel button
    cy.contains('button', 'Cancel').click();
    
    // Verify dialog is closed
    cy.contains('Download Options').should('not.exist');
    
    // Verify we're still on the resume view page
    cy.url().should('include', '/people/1/resumes/1');
  });
});
