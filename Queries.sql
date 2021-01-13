-- Get StudentGrades with names
SELECT baseGrade.Id, baseGrade.Name, student.Id, student.Name, studentGrade.Grade
    FROM Grades baseGrade
    LEFT JOIN StudentGrades studentGrade
    ON baseGrade.Id = studentGrade.BaseGradeId
    LEFT JOIN Students student
    ON student.Id = studentGrade.StudentId

-- Create database
CREATE DATABASE Teach

-- Delete database
USE master;
DROP DATABASE Teach

-- Delete X table
DROP TABLE X

-- Insert entry into table
INSERT INTO Students VALUES (
    NEWID(),
    9999,
    'Example Student',
    '54138014080'
)

-- Delete all entries from table
DELETE FROM Students

-- Update value in entry
UPDATE Students
    SET Name='New Name'
    WHERE Id='00000000-0000-0000-0000-000000000000'

-- Get all columns from table
SELECT * FROM Students

-- Get all columns from entries with corresponding ID
SELECT * FROM Students WHERE Id='00000000-0000-0000-0000-000000000000'