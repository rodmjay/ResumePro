describe('Skills Management Tests', () => {
  beforeEach(() => {
    // Visit the app before each test
    cy.visit('/');
  });

  it('should navigate to skills management page', () => {
    // Navigate to resume edit page first
    cy.visit('/people/1/resumes/1/edit');
    
    // Click on the skills tab or button
    cy.contains('Skills').click();
    
    // Verify we're on the skills management section
    cy.contains('Manage Skills').should('be.visible');
  });

  it('should display existing skills', () => {
    // Navigate to skills management page
    cy.visit('/people/1/resumes/1/edit');
    cy.contains('Skills').click();
    
    // Verify skills list is displayed
    cy.get('.skills-list').should('be.visible');
    
    // There should be at least one skill displayed
    cy.get('.skill-item').should('have.length.at.least', 1);
  });

  it('should add a new skill', () => {
    // Navigate to skills management page
    cy.visit('/people/1/resumes/1/edit');
    cy.contains('Skills').click();
    
    // Click add skill button
    cy.contains('button', 'Add Skill').click();
    
    // Verify add skill dialog is displayed
    cy.contains('Add New Skill').should('be.visible');
    
    // Fill in skill details
    const newSkill = 'Cypress Testing ' + Date.now();
    cy.get('#skillName').type(newSkill);
    cy.get('#proficiencyLevel').click();
    cy.contains('Expert').click();
    
    // Save the new skill
    cy.contains('button', 'Save').click();
    
    // Verify success message
    cy.contains('Skill added successfully').should('be.visible');
    
    // Verify the new skill appears in the list
    cy.contains(newSkill).should('be.visible');
  });

  it('should edit an existing skill', () => {
    // Navigate to skills management page
    cy.visit('/people/1/resumes/1/edit');
    cy.contains('Skills').click();
    
    // Click edit button on the first skill
    cy.get('.skill-item').first().find('button[aria-label="Edit"]').click();
    
    // Verify edit skill dialog is displayed
    cy.contains('Edit Skill').should('be.visible');
    
    // Update skill details
    const updatedSkill = 'Updated Skill ' + Date.now();
    cy.get('#skillName').clear().type(updatedSkill);
    
    // Save the updated skill
    cy.contains('button', 'Save').click();
    
    // Verify success message
    cy.contains('Skill updated successfully').should('be.visible');
    
    // Verify the updated skill appears in the list
    cy.contains(updatedSkill).should('be.visible');
  });

  it('should delete a skill', () => {
    // Navigate to skills management page
    cy.visit('/people/1/resumes/1/edit');
    cy.contains('Skills').click();
    
    // Get the text of the first skill for later verification
    let skillText;
    cy.get('.skill-item').first().invoke('text').then(text => {
      skillText = text;
    });
    
    // Click delete button on the first skill
    cy.get('.skill-item').first().find('button[aria-label="Delete"]').click();
    
    // Verify confirmation dialog is displayed
    cy.contains('Confirm Deletion').should('be.visible');
    
    // Confirm deletion
    cy.contains('button', 'Yes, Delete').click();
    
    // Verify success message
    cy.contains('Skill deleted successfully').should('be.visible');
    
    // Verify the skill no longer appears in the list
    cy.contains(skillText).should('not.exist');
  });

  it('should cancel skill addition', () => {
    // Navigate to skills management page
    cy.visit('/people/1/resumes/1/edit');
    cy.contains('Skills').click();
    
    // Click add skill button
    cy.contains('button', 'Add Skill').click();
    
    // Verify add skill dialog is displayed
    cy.contains('Add New Skill').should('be.visible');
    
    // Fill in skill details
    cy.get('#skillName').type('Skill to Cancel');
    
    // Click cancel button
    cy.contains('button', 'Cancel').click();
    
    // Verify dialog is closed
    cy.contains('Add New Skill').should('not.exist');
    
    // Verify the skill was not added
    cy.contains('Skill to Cancel').should('not.exist');
  });
});
