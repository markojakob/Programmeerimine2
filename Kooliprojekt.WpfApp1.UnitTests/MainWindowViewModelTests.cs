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
        public async Task SaveCommand_saves_car_when_car_is_not_null()
        {
            // Arrange
            _vm.SelectedItem = new Car();

            // Act
            _vm.SaveCommand.Execute(_vm.SelectedItem);
            _apiMock.Setup(list => list.Save(_vm.SelectedItem));
            await _vm.Load();


            // Assert
            Assert.NotNull(_vm.SelectedItem);
        }

        [Fact]
        public async Task DeleteCommand_deletes_car_when_confirmDelete_is_not_null()
        {
            _vm.SelectedItem = new Car();
            _vm.DeleteCommand.Execute(_vm.SelectedItem);
            
            Assert.Null(_vm.SelectedItem);


        }
    }
}