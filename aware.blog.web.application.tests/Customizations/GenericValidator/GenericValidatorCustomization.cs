using System;

using AutoFixture;

using Moq;

namespace Aware.Blog.Tests.GenericValidator
{
    public class GenericValidatorCustomization : ICustomization
    {
        public Mock<IServiceProvider> ServiceProvider { get; private set; }

        public virtual void Customize(IFixture fixture)
        {
            ServiceProvider = fixture.Freeze<Mock<IServiceProvider>>();

            var fixtureCreateMethod = fixture.GetType()
                .GetMethod(nameof(fixture.Create));

            ServiceProvider.Setup(x => x.GetService(It.IsAny<Type>()))
                .Returns(new InvocationFunc((invocation) => fixtureCreateMethod.MakeGenericMethod(invocation.Method.GetGenericArguments()[0]).Invoke(fixture, new object[0])));
        }
    }
}