describe('Company Creation Tests', () => {
  beforeEach(() => {
    // Visit the app before each test
    cy.visit('/');
    
    // Navigate to people page and select a person
    cy.contains('span', 'People').click();
    cy.get('table tbody tr').first().click();
    
    // Click on the Companies tab or button
    cy.contains('Companies').click();
  });

  it('should open company creation form when add button is clicked', () => {
    // Click the add company button
    cy.contains('button', 'Add Company').click();
    
    // Verify the create dialog is displayed
    cy.contains('Create Company').should('be.visible');
    cy.get('#company').should('be.visible');
    cy.get('#location').should('be.visible');
  });

  it('should validate required fields in the company form', () => {
    // Click the add company button
    cy.contains('button', 'Add Company').click();
    
    // Try to save without filling required fields
    cy.contains('button', 'Save').click();
    
    // Verify validation errors are displayed
    cy.contains('Company name is required').should('be.visible');
    cy.contains('Location is required').should('be.visible');
    cy.contains('Start date is required').should('be.visible');
  });

  it('should create a company successfully', () => {
    // Click the add company button
    cy.contains('button', 'Add Company').click();
    
    // Fill in company details
    const companyName = 'Test Company ' + Date.now();
    cy.get('#company').type(companyName);
    cy.get('#description').type('Test company description');
    cy.get('#location').type('Test City, Test State');
    
    // Set start date
    cy.get('#startDate').click();
    cy.get('.p-datepicker-today').click();
    
    // Save the new company
    cy.contains('button', 'Save').click();
    
    // Verify success message
    cy.contains('Company created successfully').should('be.visible');
    
    // Verify the new company appears in the list
    cy.contains(companyName).should('be.visible');
  });

  it('should handle server errors when creating a company', () => {
    // Click the add company button
    cy.contains('button', 'Add Company').click();
    
    // Fill in company details
    const companyName = 'Error Company ' + Date.now();
    cy.get('#company').type(companyName);
    cy.get('#location').type('Error City, Error State');
    
    // Set start date
    cy.get('#startDate').click();
    cy.get('.p-datepicker-today').click();
    
    // Intercept the API call and force an error response
    cy.intercept('POST', '**/api/people/*/companies', {
      statusCode: 500,
      body: {
        message: 'Server error occurred while creating company'
      }
    }).as('createCompanyError');
    
    // Save the new company
    cy.contains('button', 'Save').click();
    
    // Wait for the intercepted request
    cy.wait('@createCompanyError');
    
    // Verify error message is displayed
    cy.contains('Failed to create company').should('be.visible');
    
    // Verify we're still on the create form
    cy.contains('Create Company').should('be.visible');
  });

  it('should handle validation errors from server when creating a company', () => {
    // Click the add company button
    cy.contains('button', 'Add Company').click();
    
    // Fill in company details
    const companyName = 'Validation Error Company ' + Date.now();
    cy.get('#company').type(companyName);
    cy.get('#location').type('Validation City, Error State');
    
    // Set start date
    cy.get('#startDate').click();
    cy.get('.p-datepicker-today').click();
    
    // Intercept the API call and force a validation error response
    cy.intercept('POST', '**/api/people/*/companies', {
      statusCode: 400,
      body: {
        errors: {
          'Company': ['Company name already exists'],
          'EndDate': ['End date must be after start date']
        }
      }
    }).as('validationError');
    
    // Save the new company
    cy.contains('button', 'Save').click();
    
    // Wait for the intercepted request
    cy.wait('@validationError');
    
    // Verify validation errors from server are displayed
    cy.contains('Company name already exists').should('be.visible');
    cy.contains('End date must be after start date').should('be.visible');
    
    // Verify we're still on the create form
    cy.contains('Create Company').should('be.visible');
  });
});
