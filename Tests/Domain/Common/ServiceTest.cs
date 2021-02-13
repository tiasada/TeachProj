using Xunit;
using Moq;
using Domain.Common;
using System;

namespace Tests.Domain.Common
{
    public class ServiceTest
    {
        private Mock<IRepository<Entity>> _repository;
        private Service<Entity> _service;

        public ServiceTest()
        {
            _repository = new Mock<IRepository<Entity>>();
            _service = new Service<Entity>(_repository.Object);
        }

        [Fact]
        public void should_not_create_user_when_has_validation_errors()
        {
            _repository.Setup(x => x.Get(It.IsAny<Func<Entity,bool>>())).Returns(null as Entity);
            
            var resp = _service.Remove(Guid.NewGuid());

            Assert.False(resp);

            _repository.Verify(
                x => x.Remove(It.IsAny<Entity>()),
                Times.Never()
            );
        }
    }
}
