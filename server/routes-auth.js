const express = require('express');
const bcrypt = require('bcryptjs');
//console.log("Register request body:", req.body);
const db = require('./db'); // mysql2/promise pool from db.js

const router = express.Router();

// REGISTER (all fields)
router.post('/register', async (req, res) => {
  const {
    StudentID,
    StudentNumber,
    FullName,
    DOB,
    Address,
    email,
    AccountType,
    password
  } = req.body;

  // basic server-side validation
  if (!StudentID || !StudentNumber || !FullName || !DOB || !Address || !email || !AccountType || !password) {
    return res.status(400).json({ error: 'All fields are required.' });
  }

  try {
    const hashedPassword = await bcrypt.hash(password, 10);

    await db.query(
      `INSERT INTO users
       (StudentID, StudentNumber, FullName, DOB, Address, email, AccountType, password)
       VALUES (?, ?, ?, ?, ?, ?, ?, ?)`,
      [StudentID, StudentNumber, FullName, DOB, Address, email, AccountType, hashedPassword]
    );

    return res.json({ message: '✅ Registration successful! Please log in.' });
  } catch (err) {
    // handle duplicate keys nicely
    if (err && err.code === 'ER_DUP_ENTRY') {
      return res.status(400).json({ error: 'Student Number or Email already registered.' });
    }
    console.error('Registration error:', err);
    return res.status(500).json({ error: '⚠️ Registration failed' });
  }
});

// LOGIN
router.post('/login', async (req, res) => {
  const { StudentNumber, password } = req.body;

  if (!StudentNumber || !password) {
    return res.status(400).json({ error: 'Student Number and Password are required.' });
  }

  try {
    const [rows] = await db.query(
      'SELECT * FROM users WHERE StudentNumber = ? ',
      [StudentNumber]
    );

    if (!rows || rows.length === 0) {
      return res.status(400).json({ error: '❌ User not found' });
    }

    const user = rows[0];
    const valid = await bcrypt.compare(password, user.password);
    if (!valid) {
      return res.status(400).json({ error: '❌ Invalid password' });
    }

    // (Optional) set a session/JWT here
    return res.json({ message: '✅ Login successful!' });
  } catch (err) {
    console.error('Login error:', err);
    return res.status(500).json({ error: '⚠️ Login failed' });
  }
});

module.exports = router;