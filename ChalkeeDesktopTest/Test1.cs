using ChalkeeDesktopLib;
namespace ChalkeeDesktopTest;

[TestClass]
public sealed class UnitTest
{
    [TestMethod]
    public void UserCreationTest()
    {
        User user = new(Guid.NewGuid(), "test@example.com", "password", "Test", "Name", Guid.NewGuid(), Guid.NewGuid(), "Student", "7xxxxxxxxxx");
        Assert.AreEqual("Test", user.FirstName);
    }

    [TestMethod]
    public void ReturnsWithAMyInformation()
    {
        User testUser = new(Guid.NewGuid(), "test@example.com", "password", "Test", "Name", Guid.NewGuid(), Guid.NewGuid(), "Student", "7xxxxxxxxxx");
        
        //Assert.ThrowsExceptionAsync<Exception>(
          //  () =>
            //{
                
            //}
            
            
            //);
        
    }




}
