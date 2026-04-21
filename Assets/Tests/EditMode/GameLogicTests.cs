using NUnit.Framework;

public class GameLogicTests
{
    [Test]
    public void SceneManagerImport_DoesNotThrow()
    {
        var sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        Assert.IsNotNull(sceneName);
    }

    [Test]
    public void PlayerController_Directions_HasExpectedValues()
    {
        Assert.AreEqual(4, System.Enum.GetValues(typeof(PlayerController.Directions)).Length);
    }
}
