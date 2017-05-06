using System;
using System.Collections.Generic;
using System.Web.Mvc;
using _3NetEventManagerS.Models;

namespace _3NetEventManagerS.Controllers
{
    public class SampleDataController : Controller
    {
        readonly RoleController _roleController = new RoleController();
        readonly UserController _userController = new UserController();
        readonly EventTypeController _eventTypeController = new EventTypeController();
        readonly EventStatusController _eventStatusController = new EventStatusController();
        readonly EventController _eventController = new EventController();
        readonly ContributionTypeController _contributionTypeController = new ContributionTypeController();
        readonly ContributionController _contributionController = new ContributionController();

        public String Index()
        {
            var roles = new List<Role> {new Role {Name = "Admin"}, new Role {Name = "User"}};

            var users = new List<User>
            {
                new User {FirstName = "Admin", LastName = "Root", NickName = "Admin", Password = "Admin", RoleId = 1},
                new User {FirstName = "Paul", LastName = "McCartney", NickName = "Paul", Password = "Paul", RoleId = 2},
                new User {FirstName = "Ringo", LastName = "Starr", NickName = "Ringo", Password = "Ringo", RoleId = 2},
                new User
                {
                    FirstName = "Georges",
                    LastName = "Harrison",
                    NickName = "Georges",
                    Password = "Georges",
                    RoleId = 2
                },
                new User {FirstName = "John", LastName = "Lennon", NickName = "John", Password = "John", RoleId = 2}
            };

            var eventTypes = new List<EventType>
            {
                new EventType {Name = "Lunch"},
                new EventType {Name = "Party"},
                new EventType {Name = "Meeting"},
                new EventType {Name = "RPG"}
            };

            var eventStatuses = new List<EventStatus>
            {
                new EventStatus {Name = "Open"},
                new EventStatus {Name = "Closed"},
                new EventStatus {Name = "Pending"}
            };

            var events = new List<Event>
            {
                new Event
                {
                    Name = "Lunch",
                    Address = "At home",
                    CreatorId = 4,
                    Description = "Lunch at home",
                    EventStatusId = 2,
                    EventTypeId = 1,
                    Date = new DateTime(2014, 3, 2).Date,
                    Time = "20h00"
                },
                new Event
                {
                    Name = "Party",
                    Address = "At club",
                    CreatorId = 5,
                    Description = "Party at club",
                    EventStatusId = 3,
                    EventTypeId = 2,
                    Date = new DateTime(2014, 6, 12).Date,
                    Time = "22h00"
                },
                new Event
                {
                    Name = "Stuff",
                    Address = "56 rue Fontvieille",
                    CreatorId = 3,
                    Description = "Stuff at 56 rue Fontvieille",
                    EventStatusId = 3,
                    EventTypeId = 3,
                    Date = new DateTime(2015, 1, 4).Date,
                    Time = "19h00"
                },
                new Event
                {
                    Name = "Role Playing Game",
                    Address = "At store",
                    CreatorId = 2,
                    Description = "RPG at store",
                    EventStatusId = 1,
                    EventTypeId = 4,
                    Date = new DateTime(2014, 4, 17).Date,
                    Time = "14h30"
                }
            };

            var contributionTypes = new List<ContributionType>
            {
                new ContributionType {Name = "Food"},
                new ContributionType {Name = "Beverage"},
                new ContributionType {Name = "Money"}
            };

            var contributions = new List<Contribution>
            {
                new Contribution {Name = "Food for Lunch", ContributionTypeId = 1, Quantity = 2, EventId = 1},
                new Contribution {Name = "Beverage for party", ContributionTypeId = 2, Quantity = 5, EventId = 2},
                new Contribution {Name = "Money for stuff", ContributionTypeId = 3, Quantity = 120, EventId = 3},
                new Contribution {Name = "Food for RPG", ContributionTypeId = 1, Quantity = 53, EventId = 4}
            };

            foreach (var role in roles)
            {
                _roleController.Create(role);
            }

            foreach (var user in users)
            {
                _userController.Create(user);
            }

            foreach (var eventType in eventTypes)
            {
                _eventTypeController.Create(eventType);
            }

            foreach (var eventStatus in eventStatuses)
            {
                _eventStatusController.Create(eventStatus);
            }

            foreach (var event1 in events)
            {
                _eventController.Create(event1);
            }

            foreach (var contributionType in contributionTypes)
            {
                _contributionTypeController.Create(contributionType);
            }

            foreach (var contribution in contributions)
            {
                _contributionController.Create(contribution);
            }

            return "Database populated.";
        }

    }
}
