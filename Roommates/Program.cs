using System;
using System.Collections.Generic;
using Roommates.Models;
using Roommates.Repositories;

namespace Roommates
{
    class Program
    {
        /// <summary>
        ///  This is the address of the database.
        ///  We define it here as a constant since it will never change.
        /// </summary>
        private const string CONNECTION_STRING = @"server=localhost\SQLExpress;database=Roommates;integrated security=true";

        static void Main(string[] args)
        {
            RoomRepository roomRepo = new RoomRepository(CONNECTION_STRING);
            RoommateRepository roommateRepo = new RoommateRepository(CONNECTION_STRING);

            Console.WriteLine(@"
Welcome, what would you like to do today? 
1: View or edit rooms
2: View or edit roommates
");
            int response = int.Parse(Console.ReadLine());
            switch (response)
            {
                case 1:
                    Console.WriteLine(@"
                    What would you like to do?
                    1: View all rooms
                    2: View individual room info
                    3: Add a room
                    4: Edit room info
                    5: Delete a room");
                    int roomResponse = int.Parse(Console.ReadLine());
                    switch (roomResponse)
                    {
                        case 1:
                            Console.WriteLine("Getting All Rooms:");
                            Console.WriteLine();

                            List<Room> allRoommates = roomRepo.GetAll();

                            foreach (Room room in allRoommates)
                            {
                                Console.WriteLine($"{room.Name} has an Id of {room.Id} and a max occupancy of {room.MaxOccupancy}");
                            }
                            break;
                        case 2:
                            Console.WriteLine("Enter a room number:");
                            int roomNum = int.Parse(Console.ReadLine());
                            Room singleRoom = roomRepo.GetById(roomNum);
                            Console.WriteLine($"{singleRoom.Name}, Max occupancy: {singleRoom.MaxOccupancy}");
                            break;
                        case 3:
                            Console.WriteLine("Enter room name:");
                            string newRoomName = Console.ReadLine();
                            Console.WriteLine($"Enter {newRoomName}'s max occupancy:");
                            int newRoomOccupancy = int.Parse(Console.ReadLine());
                            Room newRoom = new Room()
                            {
                                Name = newRoomName,
                                MaxOccupancy = newRoomOccupancy
                            };
                            roomRepo.Insert(newRoom);
                            Console.WriteLine($"Added {newRoomName}.");
                            break;
                        case 4:
                            Console.WriteLine("Enter a room number:");
                            int editNum = int.Parse(Console.ReadLine());
                            Console.WriteLine("Enter the room's name:");
                            string newName = Console.ReadLine();
                            Console.WriteLine("Enter the room's max occupancy:");
                            int newOcc = int.Parse(Console.ReadLine());
                            Room edittedRoom = new Room()
                            {
                                Name = newName,
                                MaxOccupancy = newOcc,
                                Id = editNum
                            };
                            roomRepo.Update(edittedRoom);
                            Console.WriteLine($"Updated {newName} info");
                            break;
                        case 5:
                            Console.WriteLine("Enter a room number:");
                            roomRepo.Delete(int.Parse(Console.ReadLine()));
                            Console.WriteLine("Deleted");
                            break;
                    }
                    break;
                case 2:
                    Console.WriteLine(@"
                    What would you like to do?
                    1: View all roommates
                    2: View individual roommate info
                    3: Add a roommate
                    4: Edit roommate info
                    5: Delete a roommate
                    6: View all roommates in specific room");
                    int roommateResponse = int.Parse(Console.ReadLine());
                    switch (roommateResponse)
                    {
                        case 1:
                            Console.WriteLine("Getting All Roommates:");
                            Console.WriteLine();

                            List<Roommate> allRoommates = roommateRepo.GetAll();

                            foreach (Roommate roommate in allRoommates)
                            {
                                Console.WriteLine($@"
                                {roommate.Firstname}{roommate.Lastname}
                                ID: {roommate.Id} 
                                Rent portion: {roommate.RentPortion}
                                Move in date: {roommate.MovedInDate}");
                            }
                            break;
                        case 2:
                            Console.WriteLine("Enter a roommate number:");
                            int roommateNum = int.Parse(Console.ReadLine());
                            Roommate singleRoommate = roommateRepo.GetById(roommateNum);
                            Console.WriteLine($@"
                                {singleRoommate.Firstname}{singleRoommate.Lastname}
                                ID: {singleRoommate.Id} 
                                Rent portion: {singleRoommate.RentPortion}
                                Move in date: {singleRoommate.MovedInDate}");
                            break;
                        case 3:
                            Console.WriteLine("Enter roommate's first name:");
                            string newRoommateFirstName = Console.ReadLine();
                            Console.WriteLine($"Enter {newRoommateFirstName}'s last name:");
                            string newRoommateLastName = Console.ReadLine();
                            Console.WriteLine($"Enter {newRoommateFirstName}'s rent portion:");
                            int newRoommateRent = int.Parse(Console.ReadLine());
                            Console.WriteLine($"When did they move in?");
                            DateTime newRoommateMove = DateTime.Parse(Console.ReadLine());
                            Console.WriteLine("Enter room ID:");
                            int newRoommateRoomId = int.Parse(Console.ReadLine());
                            Room newRoommateRoom = roomRepo.GetById(newRoommateRoomId);

                            Roommate newRoommate = new Roommate()
                            {
                                Firstname = newRoommateFirstName,
                                Lastname = newRoommateLastName,
                                RentPortion = newRoommateRent,
                                MovedInDate = newRoommateMove,
                                Room = newRoommateRoom
                            };
                            roommateRepo.Insert(newRoommate);
                            Console.WriteLine($"Added {newRoommateFirstName}.");
                            break;
                        case 4:
                            Console.WriteLine("Enter roommate ID:");
                            int editRoommateId = int.Parse(Console.ReadLine());
                            Console.WriteLine("Enter roommate's first name:");
                            string editRoommateFirstName = Console.ReadLine();
                            Console.WriteLine($"Enter {editRoommateFirstName}'s last name:");
                            string editRoommateLastName = Console.ReadLine();
                            Console.WriteLine($"Enter {editRoommateFirstName}'s rent portion:");
                            int editRoommateRent = int.Parse(Console.ReadLine());
                            Console.WriteLine($"When did they move in?");
                            DateTime editRoommateMove = DateTime.Parse(Console.ReadLine());
                            Console.WriteLine("Enter room ID:");
                            int editRoommateRoomId = int.Parse(Console.ReadLine());
                            Room editRoommateRoom = roomRepo.GetById(editRoommateRoomId);

                            Roommate editRoommate = new Roommate()
                            {
                                Id = editRoommateId,
                                Firstname = editRoommateFirstName,
                                Lastname = editRoommateLastName,
                                RentPortion = editRoommateRent,
                                MovedInDate = editRoommateMove,
                                Room = editRoommateRoom
                            };
                            roommateRepo.Update(editRoommate);
                            Console.WriteLine($"Updated {editRoommateFirstName}'s info.");
                            break;
                        case 5:
                            Console.WriteLine("Enter a roommate ID:");
                            roommateRepo.Delete(int.Parse(Console.ReadLine()));
                            Console.WriteLine("Deleted");
                            break;
                        case 6:
                            Console.WriteLine("Enter the room's ID:");
                            int singleRoomId = int.Parse(Console.ReadLine());
                            List<Roommate> roomMembers = roommateRepo.GetRoommatesByRoomId(singleRoomId);
                            foreach (Roommate roommate in roomMembers)
                            {
                                Console.WriteLine($@"
                                {roommate.Firstname}{roommate.Lastname}
                                ID: {roommate.Id} 
                                Rent portion: {roommate.RentPortion}
                                Move in date: {roommate.MovedInDate}");
                            }

                            ;
                            break;

                            
                    }
                    break;
                    ;
            }





            //Console.WriteLine("Getting All Roommmates:");
            //Console.WriteLine();

            //List<Roommate> allRoommates = roommateRepo.GetAll();

            
                //}
                //break;






                //Roommate newRoommate = new Roommate()
                //{
                //    Firstname = "Wes",
                //    Lastname = "Harrison",
                //    RentPortion = 20,
                //    MovedInDate = DateTime.Parse("1/1/2021"),
                //    Room = backBedroom
                //};
                //roommateRepo.Insert(newRoommate);

                //Roommate updatedRoommate = new Roommate()
                //{
                //    Firstname = "Wes",
                //    Lastname = "Harrison",
                //    RentPortion = 30,
                //    MovedInDate = DateTime.Parse("1/1/2021"),
                //    Room = backBedroom,
                //    Id = 4
                //};

            }

    }
}