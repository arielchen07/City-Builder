const mongoose = require('mongoose');
const Schema = mongoose.Schema;
const bcrypt = require('bcryptjs');

const userSchema = new Schema({
    name: {
        type: String
    },
    email: {
        type: String
    },
    password: {
        type: String
    },
    status: {
        type: String,
        default: 'offline'
    },
    items: {
      type: [String]
    }
}, { timestamps: true });


userSchema.pre('save', function (next) {
    const user = this;
    if (!user.isModified('password')) return next();
  
    bcrypt.genSalt(10, function (err, salt) {
      if (err) return next(err);
  
      bcrypt.hash(user.password, salt, function (err, hash) {
        if (err) return next(err);
  
        user.password = hash
        next();
      })
  
    })
  
  })

  userSchema.statics.findByCredentials = async function (email, password) {
    const user = await User.findOne({ email });
    if (!user) throw new Error('invalid email or password');
  
    const isMatch = bcrypt.compare(password, user.password);
    if (!isMatch) throw new Error('invalid email or password')
    return user
  }
  

const User = mongoose.model('User', userSchema);

module.exports = User;
