const chai = require('chai');
const expect = chai.expect;
const supertest = require('supertest');
const app = require('../server'); 
const User = require('../models/UserModel'); 
const {register, login, userProfile} = require('../controllers/UserController'); 
const UserRoute = require('../routes/UserRoute')

// Create a request agent for testing
const request = supertest(app); 

// Test suite for User Registration
describe('User Registration Test', () => {
  it('should successfully register a new user', async () => {
    const newUser = {
      name: 'NewUser10',
      email: 'NewUser@example.com',
      password: 'password123'
    };

    const response = await request
      .post('/api/register')
      .send(newUser)
      .expect(201);
    userID = response.body.userID;

    expect(response.body).to.be.an('object');
    expect(response.body.userID).to.equal(userID);
   
    

   
    await User.deleteOne({ email: newUser.email });
  });

  it('should return an error for an already registered user', async () => {
    const existingUser = {
      name: 'testUser10',
      email: 'TestUser10@example.com',
      password: 'password456',
    };

    
    await User.create(existingUser);

    const response = await request
      .post('/api/register')
      .send(existingUser)
      .expect(400);

    expect(response.body).to.be.an('object');
    expect(response.body.error).to.equal('User with this email already exists');

    
    await User.deleteOne({ email: existingUser.email });
  });
});