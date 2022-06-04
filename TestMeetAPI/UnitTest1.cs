using AutoMapper;
using FluentAssertions;
using Meetup.Controllers;
using Meetup.DAO.Interfaces;
using Meetup.Data.Entities;
using Meetup.Models;
using Meetup.Models.Event;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestMeetAPI
{
    public class Tests
    {
        Mock<IBaseDAO<Event, EventViewModel, EventPostModel, EventPutModel>> mockEvents = new Mock<IBaseDAO<Event, EventViewModel, EventPostModel, EventPutModel>>();
        IMapper _mapper;
        EventPostModel postModel = new EventPostModel()
        {
            Description = "Post",
            Name = "Post",
            EventOrganizerId = Guid.Parse("3736A601-D9EE-4CE1-8504-73E103D8DB3A"),
            EventPlaceId = Guid.Parse("3736A601-D9EE-4CE1-8504-73E103D8DB3A"),
            EventSpeakerId = Guid.Parse("3736A601-D9EE-4CE1-8504-73E103D8DB3A"),
            Time = DateTime.Now.AddDays(6)
        };
        [SetUp]
        public void Setup()
        {
            var mappConfig = new MapperConfiguration(x =>
             {
                 x.CreateMap<Event, EventViewModel>().ReverseMap();
             });
            _mapper = mappConfig.CreateMapper();
            mockEvents.Setup(x => x.Get(Guid.Parse("3736A601-D9EE-4CE1-8504-73E103D8DB3A"), nameof(Event.EventOrganizer), nameof(Event.EventSpeaker), nameof(Event.EventPlace)))
                .ReturnsAsync(new EventViewModel()
                {
                    Id = Guid.Parse("3736A601-D9EE-4CE1-8504-73E103D8DB3A"),
                    Description = "Test",
                    Name = "Test"               
                });
            mockEvents.Setup(x => x.Get(Guid.Empty)).ReturnsAsync(default(EventViewModel));            
            mockEvents.Setup(x => x.Post(postModel)).ReturnsAsync(Guid.Parse("A20EDB33-FC1F-4A42-91FF-5B3DB18BE947"));
            mockEvents.Setup(x => x.Delete(Guid.Parse("A20EDB33-FC1F-4A42-91FF-5B3DB18BE947"))).ReturnsAsync(true);
            mockEvents.Setup(x => x.Delete(Guid.Parse("A30EDB33-FC1F-4A42-91FF-5B3DB18BE947"))).ReturnsAsync(false);

        }

        [Test]
        public async Task TestGetExistingObject()
        {
            EventController eventController = new EventController(null, mockEvents.Object);
            var result = await eventController.Get(Guid.Parse("3736A601-D9EE-4CE1-8504-73E103D8DB3A"));
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult).Value.Should().BeEquivalentTo(new EventViewModel()
            {
                Id = Guid.Parse("3736A601-D9EE-4CE1-8504-73E103D8DB3A"),
                Description = "Test",
                Name = "Test"
            });
        }
        [Test]
        public async Task TestGetNoExistingObject()
        {
            EventController eventController = new EventController(null, mockEvents.Object);
            var result = await eventController.Get(Guid.Empty);
            result.Should().BeOfType<BadRequestResult>();
        }
        [Test]
        public async Task TestPost()
        {
            EventController eventController = new EventController(null, mockEvents.Object);           
            var result = await eventController.Post(postModel);
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult).Value.Should().BeEquivalentTo(Guid.Parse("A20EDB33-FC1F-4A42-91FF-5B3DB18BE947"));
        }
        [Test]
        public async Task TestDeleteExistingObject()
        {
            EventController eventController = new EventController(null, mockEvents.Object);
            var result = await eventController.Delete(Guid.Parse("A20EDB33-FC1F-4A42-91FF-5B3DB18BE947"));
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult).Value.Should().BeEquivalentTo(true);
        }
        [Test]
        public async Task TestDeleteNoExistingObject()
        {
            EventController eventController = new EventController(null, mockEvents.Object);
            var result = await eventController.Delete(Guid.Parse("A30EDB33-FC1F-4A42-91FF-5B3DB18BE947"));
            result.Should().BeOfType<BadRequestResult>();
        }
    }
}