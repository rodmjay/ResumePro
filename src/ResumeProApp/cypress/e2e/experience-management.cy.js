describe('Work Experience Management Tests', () => {
  beforeEach(() => {
    // Visit the app before each test
    cy.visit('/');
  });

  it('should navigate to experience management page', () => {
    // Navigate to resume edit page first
    cy.visit('/people/1/resumes/1/edit');
    
    // Click on the experience tab or button
    cy.contains('Experience').click();
    
    // Verify we're on the experience management section
    cy.contains('Manage Work Experience').should('be.visible');
  });

  it('should display existing experience entries', () => {
    // Navigate to experience management page
    cy.visit('/people/1/resumes/1/edit');
    cy.contains('Experience').click();
    
    // Verify experience list is displayed
    cy.get('.experience-list').should('be.visible');
    
    // There should be at least one experience entry displayed
    cy.get('.experience-item').should('have.length.at.least', 1);
  });

  it('should add a new experience entry', () => {
    // Navigate to experience management page
    cy.visit('/people/1/resumes/1/edit');
    cy.contains('Experience').click();
    
    // Click add experience button
    cy.contains('button', 'Add Experience').click();
    
    // Verify add experience dialog is displayed
    cy.contains('Add Work Experience').should('be.visible');
    
    // Fill in experience details
    const company = 'Test Company ' + Date.now();
    cy.get('#company').type(company);
    cy.get('#position').type('Software Engineer');
    cy.get('#startDate').type('2020-01-01');
    cy.get('#endDate').type('2023-12-31');
    cy.get('#description').type('Developed and maintained web applications');
    
    // Save the new experience entry
    cy.contains('button', 'Save').click();
    
    // Verify success message
    cy.contains('Experience added successfully').should('be.visible');
    
    // Verify the new experience entry appears in the list
    cy.contains(company).should('be.visible');
  });

  it('should edit an existing experience entry', () => {
    // Navigate to experience management page
    cy.visit('/people/1/resumes/1/edit');
    cy.contains('Experience').click();
    
    // Click edit button on the first experience entry
    cy.get('.experience-item').first().find('button[aria-label="Edit"]').click();
    
    // Verify edit experience dialog is displayed
    cy.contains('Edit Work Experience').should('be.visible');
    
    // Update experience details
    const updatedCompany = 'Updated Company ' + Date.now();
    cy.get('#company').clear().type(updatedCompany);
    
    // Save the updated experience entry
    cy.contains('button', 'Save').click();
    
    // Verify success message
    cy.contains('Experience updated successfully').should('be.visible');
    
    // Verify the updated experience entry appears in the list
    cy.contains(updatedCompany).should('be.visible');
  });

  it('should delete an experience entry', () => {
    // Navigate to experience management page
    cy.visit('/people/1/resumes/1/edit');
    cy.contains('Experience').click();
    
    // Get the text of the first experience entry for later verification
    let experienceText;
    cy.get('.experience-item').first().invoke('text').then(text => {
      experienceText = text;
    });
    
    // Click delete button on the first experience entry
    cy.get('.experience-item').first().find('button[aria-label="Delete"]').click();
    
    // Verify confirmation dialog is displayed
    cy.contains('Confirm Deletion').should('be.visible');
    
    // Confirm deletion
    cy.contains('button', 'Yes, Delete').click();
    
    // Verify success message
    cy.contains('Experience deleted successfully').should('be.visible');
    
    // Verify the experience entry no longer appears in the list
    cy.contains(experienceText).should('not.exist');
  });

  it('should validate required fields in experience form', () => {
    // Navigate to experience management page
    cy.visit('/people/1/resumes/1/edit');
    cy.contains('Experience').click();
    
    // Click add experience button
    cy.contains('button', 'Add Experience').click();
    
    // Try to submit without filling required fields
    cy.contains('button', 'Save').click();
    
    // Verify validation errors are displayed
    cy.contains('Company is required').should('be.visible');
    cy.contains('Position is required').should('be.visible');
    cy.contains('Start date is required').should('be.visible');
  });

  it('should handle current job with no end date', () => {
    // Navigate to experience management page
    cy.visit('/people/1/resumes/1/edit');
    cy.contains('Experience').click();
    
    // Click add experience button
    cy.contains('button', 'Add Experience').click();
    
    // Fill in experience details
    const company = 'Current Company ' + Date.now();
    cy.get('#company').type(company);
    cy.get('#position').type('Current Position');
    cy.get('#startDate').type('2023-01-01');
    
    // Check "Current Job" checkbox
    cy.get('#currentJob').check();
    
    // Verify end date is disabled
    cy.get('#endDate').should('be.disabled');
    
    // Save the new experience entry
    cy.contains('button', 'Save').click();
    
    // Verify success message
    cy.contains('Experience added successfully').should('be.visible');
    
    // Verify the new experience entry appears in the list with "Present" as end date
    cy.contains(company).should('be.visible');
    cy.contains('Present').should('be.visible');
  });

  it('should cancel experience addition', () => {
    // Navigate to experience management page
    cy.visit('/people/1/resumes/1/edit');
    cy.contains('Experience').click();
    
    // Click add experience button
    cy.contains('button', 'Add Experience').click();
    
    // Verify add experience dialog is displayed
    cy.contains('Add Work Experience').should('be.visible');
    
    // Fill in some experience details
    cy.get('#company').type('Company to Cancel');
    
    // Click cancel button
    cy.contains('button', 'Cancel').click();
    
    // Verify dialog is closed
    cy.contains('Add Work Experience').should('not.exist');
    
    // Verify the experience entry was not added
    cy.contains('Company to Cancel').should('not.exist');
  });
});
