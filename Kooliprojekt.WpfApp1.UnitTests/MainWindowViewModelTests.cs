using Moq;
using System.Security.Policy;
using WpfApp1;
using WpfApp1.Api;
using Xunit;
namespace Kooliprojekt.WpfApp1.UnitTests
{
    public class MainWindowViewModelTests
    {
        private readonly Mock<IApiClient> _apiMock;
        private readonly MainWindowViewModel _vm;
        public MainWindowViewModelTests()
        {
            _apiMock = new Mock<IApiClient>();
            _vm = new MainWindowViewModel(_apiMock.Object);
        }


        [Fact]
        public void CanExecute_Returns_True_When_Selected_Item_Exists()
        {
            // Arrange
            _vm.SelectedItem = new Car();

            // Act
            var canExecute = ((RelayCommand<Car>)_vm.SaveCommand).CanExecute(null);

            // Assert
            Assert.True(canExecute);
        }


    }
}