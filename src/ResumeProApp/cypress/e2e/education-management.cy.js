describe('Education Management Tests', () => {
  beforeEach(() => {
    // Visit the app before each test
    cy.visit('/');
  });

  it('should navigate to education management page', () => {
    // Navigate to resume edit page first
    cy.visit('/people/1/resumes/1/edit');
    
    // Click on the education tab or button
    cy.contains('Education').click();
    
    // Verify we're on the education management section
    cy.contains('Manage Education').should('be.visible');
  });

  it('should display existing education entries', () => {
    // Navigate to education management page
    cy.visit('/people/1/resumes/1/edit');
    cy.contains('Education').click();
    
    // Verify education list is displayed
    cy.get('.education-list').should('be.visible');
    
    // There should be at least one education entry displayed
    cy.get('.education-item').should('have.length.at.least', 1);
  });

  it('should add a new education entry', () => {
    // Navigate to education management page
    cy.visit('/people/1/resumes/1/edit');
    cy.contains('Education').click();
    
    // Click add education button
    cy.contains('button', 'Add Education').click();
    
    // Verify add education dialog is displayed
    cy.contains('Add Education').should('be.visible');
    
    // Fill in education details
    const institution = 'Test University ' + Date.now();
    cy.get('#institution').type(institution);
    cy.get('#degree').type('Bachelor of Science');
    cy.get('#fieldOfStudy').type('Computer Science');
    cy.get('#startDate').type('2018-09-01');
    cy.get('#endDate').type('2022-06-30');
    cy.get('#description').type('Graduated with honors');
    
    // Save the new education entry
    cy.contains('button', 'Save').click();
    
    // Verify success message
    cy.contains('Education added successfully').should('be.visible');
    
    // Verify the new education entry appears in the list
    cy.contains(institution).should('be.visible');
  });

  it('should edit an existing education entry', () => {
    // Navigate to education management page
    cy.visit('/people/1/resumes/1/edit');
    cy.contains('Education').click();
    
    // Click edit button on the first education entry
    cy.get('.education-item').first().find('button[aria-label="Edit"]').click();
    
    // Verify edit education dialog is displayed
    cy.contains('Edit Education').should('be.visible');
    
    // Update education details
    const updatedInstitution = 'Updated University ' + Date.now();
    cy.get('#institution').clear().type(updatedInstitution);
    
    // Save the updated education entry
    cy.contains('button', 'Save').click();
    
    // Verify success message
    cy.contains('Education updated successfully').should('be.visible');
    
    // Verify the updated education entry appears in the list
    cy.contains(updatedInstitution).should('be.visible');
  });

  it('should delete an education entry', () => {
    // Navigate to education management page
    cy.visit('/people/1/resumes/1/edit');
    cy.contains('Education').click();
    
    // Get the text of the first education entry for later verification
    let educationText;
    cy.get('.education-item').first().invoke('text').then(text => {
      educationText = text;
    });
    
    // Click delete button on the first education entry
    cy.get('.education-item').first().find('button[aria-label="Delete"]').click();
    
    // Verify confirmation dialog is displayed
    cy.contains('Confirm Deletion').should('be.visible');
    
    // Confirm deletion
    cy.contains('button', 'Yes, Delete').click();
    
    // Verify success message
    cy.contains('Education deleted successfully').should('be.visible');
    
    // Verify the education entry no longer appears in the list
    cy.contains(educationText).should('not.exist');
  });

  it('should validate required fields in education form', () => {
    // Navigate to education management page
    cy.visit('/people/1/resumes/1/edit');
    cy.contains('Education').click();
    
    // Click add education button
    cy.contains('button', 'Add Education').click();
    
    // Try to submit without filling required fields
    cy.contains('button', 'Save').click();
    
    // Verify validation errors are displayed
    cy.contains('Institution is required').should('be.visible');
    cy.contains('Degree is required').should('be.visible');
    cy.contains('Field of study is required').should('be.visible');
  });

  it('should cancel education addition', () => {
    // Navigate to education management page
    cy.visit('/people/1/resumes/1/edit');
    cy.contains('Education').click();
    
    // Click add education button
    cy.contains('button', 'Add Education').click();
    
    // Verify add education dialog is displayed
    cy.contains('Add Education').should('be.visible');
    
    // Fill in some education details
    cy.get('#institution').type('Institution to Cancel');
    
    // Click cancel button
    cy.contains('button', 'Cancel').click();
    
    // Verify dialog is closed
    cy.contains('Add Education').should('not.exist');
    
    // Verify the education entry was not added
    cy.contains('Institution to Cancel').should('not.exist');
  });
});
