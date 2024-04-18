using System;
using Microsoft.Maui.Controls;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Hex.HexTypes;
using System.Threading.Tasks;

namespace Lidhja
{
    public partial class InsertStudentProgress : ContentPage
    {
        private Web3 web3;
        private string contractAddress = "0xF2A38Ff933461De5AC1781203b99566A4537493D"; // Replace with your contract's address
        private string abi = @"[
        {
            ""anonymous"": false,
            ""inputs"": [
                {
                    ""indexed"": false,
                    ""internalType"": ""uint256"",
                    ""name"": ""studentId"",
                    ""type"": ""uint256""
                }
            ],
            ""name"": ""StudentRecordUpdated"",
            ""type"": ""event""
        },
        {
            ""inputs"": [
                {
                    ""internalType"": ""uint256"",
                    ""name"": ""_studentId"",
                    ""type"": ""uint256""
                }
            ],
            ""name"": ""getStudentRecord"",
            ""outputs"": [
                {
                    ""components"": [
                        {
                            ""internalType"": ""uint256"",
                            ""name"": ""studentId"",
                            ""type"": ""uint256""
                        },
                        {
                            ""internalType"": ""string"",
                            ""name"": ""fullName"",
                            ""type"": ""string""
                        },
                        {
                            ""internalType"": ""string"",
                            ""name"": ""course"",
                            ""type"": ""string""
                        },
                        {
                            ""internalType"": ""string"",
                            ""name"": ""grade"",
                            ""type"": ""string""
                        },
                        {
                            ""internalType"": ""string"",
                            ""name"": ""notes"",
                            ""type"": ""string""
                        }
                    ],
                    ""internalType"": ""struct StudentRecords.Student"",
                    ""name"": """",
                    ""type"": ""tuple""
                }
            ],
            ""stateMutability"": ""view"",
            ""type"": ""function""
        },
        {
            ""inputs"": [
                {
                    ""internalType"": ""uint256"",
                    ""name"": ""_studentId"",
                    ""type"": ""uint256""
                },
                {
                    ""internalType"": ""string"",
                    ""name"": ""_fullName"",
                    ""type"": ""string""
                },
                {
                    ""internalType"": ""string"",
                    ""name"": ""_course"",
                    ""type"": ""string""
                },
                {
                    ""internalType"": ""string"",
                    ""name"": ""_grade"",
                    ""type"": ""string""
                },
                {
                    ""internalType"": ""string"",
                    ""name"": ""_notes"",
                    ""type"": ""string""
                }
            ],
            ""name"": ""setStudentRecord"",
            ""outputs"": [],
            ""stateMutability"": ""nonpayable"",
            ""type"": ""function""
        }
    ]"; // Replace with your contract's ABI
        private string senderAddress = "0x7614A679c8673C987939547A0A8116da2997E0d1"; // Replace with your Ganache account address

        public InsertStudentProgress()
        {
            InitializeComponent();

            // Initialize web3 instance pointing to your Ganache
            web3 = new Web3("http://localhost:7545");
        }

        private async void OnSaveProgressClicked(object sender, EventArgs e)
        {
            string studentId = studentIdEntry.Text;
            string fullName = fullNameEntry.Text;
            string course = courseEntry.Text;
            string grade = gradeEntry.Text;
            string notes = notesEditor.Text;

            // Basic validation for empty fields
            if (string.IsNullOrWhiteSpace(studentId) ||
                string.IsNullOrWhiteSpace(fullName) ||
                string.IsNullOrWhiteSpace(course) ||
                string.IsNullOrWhiteSpace(grade))
            {
                await DisplayAlert("Validation Error", "All fields must be filled.", "OK");
                return;
            }

            // Assuming studentId should be an integer
            if (!uint.TryParse(studentId, out uint studentIdUint))
            {
                await DisplayAlert("Error", "Student ID must be a valid number.", "OK");
                return;
            }

            try
            {
                var receipt = await AddStudentRecordAsync(
                    studentIdUint,
                    fullName,
                    course,
                    grade,
                    notes
                );

                // After successful blockchain transaction, save to local database
                var progress = new StudentProgress
                {
                    StudentId = studentId,
                    FullName = fullName,
                    Course = course,
                    Grade = grade,
                    Notes = notes
                };

                DataManager.AddStudentProgress(progress); // Save to local database

                await DisplayAlert("Success", "Student progress saved successfully both on blockchain and locally!\nTx Hash: " + receipt.TransactionHash, "OK");
                await Navigation.PopAsync(); // Navigate back to the previous page
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Blockchain transaction failed: " + ex.Message, "OK");
            }
        }

        private async Task<TransactionReceipt> AddStudentRecordAsync(
            uint studentId,
            string fullName,
            string course,
            string grade,
            string notes)
        {
            // Get the contract and function
            var contract = web3.Eth.GetContract(abi, contractAddress);
            var addStudentFunction = contract.GetFunction("setStudentRecord");

            // Send the transaction and wait for receipt
            var receipt = await addStudentFunction.SendTransactionAndWaitForReceiptAsync(
                senderAddress,
                new HexBigInteger(900000), // Gas limit - you might adjust this based on your needs
                new HexBigInteger(0), // Value in Wei
                null, // Nonce - let Nethereum manage it
                studentId,
                fullName,
                course,
                grade,
                notes
            );

            return receipt;
        }
    }
}
