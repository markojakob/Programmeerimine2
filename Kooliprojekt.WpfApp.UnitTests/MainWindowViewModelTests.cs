using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using WpfApp1.Api;
using Xunit;

namespace Kooliprojekt.WpfApp.UnitTests
{
    
    public class MainWindowViewModelTests
    {
        private readonly Mock<IApiClient> _apiCLientMock;

        public MainWindowViewModelTests()
        {
            _apiCLientMock = new Mock<IApiClient>(); 
        
        
        }
    }
}
