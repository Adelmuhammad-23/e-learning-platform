using e_learning.Services.Implementations;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text.Json;

namespace e_learning.Services.Tests
{
    public class PayPalServiceTests
    {
        private readonly Mock<IConfiguration> _configMock;
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private readonly HttpClient _httpClient;
        private readonly PayPalService _payPalService;

        public PayPalServiceTests()
        {
            _configMock = new Mock<IConfiguration>();
            _configMock.Setup(x => x["PayPal:ClientId"]).Returns("test-client-id");
            _configMock.Setup(x => x["PayPal:Secret"]).Returns("test-secret");
            _configMock.Setup(x => x["PayPal:BaseUrl"]).Returns("https://api.sandbox.paypal.com");

            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_httpMessageHandlerMock.Object);
            _payPalService = new PayPalService(_httpClient, _configMock.Object);
        }

        [Fact]
        public async Task CreateOrderAsync_ValidAmount_ReturnsApprovalUrl()
        {
            // Arrange
            var amount = 100.00m;
            var studentId = 1;

            // Mock token response
            var tokenResponse = new { access_token = "test-token" };
            var tokenJson = JsonSerializer.Serialize(tokenResponse);

            // Mock order response
            var orderResponse = new
            {
                links = new[]
                {
                    new { rel = "self", href = "https://api.sandbox.paypal.com/self" },
                    new { rel = "approve", href = "https://api.sandbox.paypal.com/approve" },
                    new { rel = "capture", href = "https://api.sandbox.paypal.com/capture" }
                }
            };
            var orderJson = JsonSerializer.Serialize(orderResponse);

            _httpMessageHandlerMock.Protected()
                .SetupSequence<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(tokenJson)
                })
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(orderJson)
                });

            // Act
            var result = await _payPalService.CreateOrderAsync(amount, studentId);

            // Assert
            Assert.Equal("https://api.sandbox.paypal.com/approve", result);
        }

        [Fact]
        public async Task IsOrderApprovedAsync_ApprovedOrder_ReturnsTrue()
        {
            // Arrange
            var orderId = "test-order-id";
            var tokenResponse = new { access_token = "test-token" };
            var tokenJson = JsonSerializer.Serialize(tokenResponse);

            var orderResponse = new { status = "APPROVED" };
            var orderJson = JsonSerializer.Serialize(orderResponse);

            _httpMessageHandlerMock.Protected()
                .SetupSequence<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(tokenJson)
                })
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(orderJson)
                });

            // Act
            var result = await _payPalService.IsOrderApprovedAsync(orderId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task IsOrderApprovedAsync_PendingOrder_ReturnsFalse()
        {
            // Arrange
            var orderId = "test-order-id";
            var tokenResponse = new { access_token = "test-token" };
            var tokenJson = JsonSerializer.Serialize(tokenResponse);

            var orderResponse = new { status = "PENDING" };
            var orderJson = JsonSerializer.Serialize(orderResponse);

            _httpMessageHandlerMock.Protected()
                .SetupSequence<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(tokenJson)
                })
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(orderJson)
                });

            // Act
            var result = await _payPalService.IsOrderApprovedAsync(orderId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task CaptureOrderAsync_ValidOrder_ReturnsTrue()
        {
            // Arrange
            var orderId = "test-order-id";
            var tokenResponse = new { access_token = "test-token" };
            var tokenJson = JsonSerializer.Serialize(tokenResponse);

            _httpMessageHandlerMock.Protected()
                .SetupSequence<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(tokenJson)
                })
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{}")
                });

            // Act
            var result = await _payPalService.CaptureOrderAsync(orderId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task CaptureOrderIfApprovedAsync_ApprovedOrder_ReturnsTrue()
        {
            // Arrange
            var orderId = "test-order-id";
            var tokenResponse = new { access_token = "test-token" };
            var tokenJson = JsonSerializer.Serialize(tokenResponse);

            var orderResponse = new { status = "APPROVED" };
            var orderJson = JsonSerializer.Serialize(orderResponse);

            _httpMessageHandlerMock.Protected()
                .SetupSequence<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(tokenJson)
                })
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(orderJson)
                })
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(tokenJson)
                })
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{}")
                });

            // Act
            var result = await _payPalService.CaptureOrderIfApprovedAsync(orderId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task CaptureOrderIfApprovedAsync_PendingOrder_ReturnsFalse()
        {
            // Arrange
            var orderId = "test-order-id";
            var tokenResponse = new { access_token = "test-token" };
            var tokenJson = JsonSerializer.Serialize(tokenResponse);

            var orderResponse = new { status = "PENDING" };
            var orderJson = JsonSerializer.Serialize(orderResponse);

            _httpMessageHandlerMock.Protected()
                .SetupSequence<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(tokenJson)
                })
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(orderJson)
                });

            // Act
            var result = await _payPalService.CaptureOrderIfApprovedAsync(orderId);

            // Assert
            Assert.False(result);
        }
    }
}