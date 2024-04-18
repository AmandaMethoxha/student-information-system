// SPDX-License-Identifier: MIT
pragma solidity ^0.8.0;

contract StudentRecords {
    // Define a structure to hold student details
    struct Student {
        uint studentId;
        string fullName;
        string course;
        string grade;
        string notes;
    }

    // Mapping from student ID to their details
    mapping(uint => Student) private students;

    // Event to be emitted on the addition or update of a student record
    event StudentRecordUpdated(uint studentId);

    // Function to add or update a student's record
    function setStudentRecord(
        uint _studentId,
        string memory _fullName,
        string memory _course,
        string memory _grade,
        string memory _notes
    ) public {
        // Create a new student record and store it in the mapping
        students[_studentId] = Student({
            studentId: _studentId,
            fullName: _fullName,
            course: _course,
            grade: _grade,
            notes: _notes
        });

        // Emit an event after updating student record
        emit StudentRecordUpdated(_studentId);
    }

    // Function to retrieve a student's record by their ID
    function getStudentRecord(uint _studentId) public view returns (Student memory) {
        require(students[_studentId].studentId != 0, "Student not found.");
        return students[_studentId];
    }
}
