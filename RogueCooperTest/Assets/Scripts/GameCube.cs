using UnityEngine;
using System.Collections;

public class GameCube : MonoBehaviour
{
    static private Shader Shader_Default = null;

    static private Material Mat_Nuetral = null;
    static private Material Mat_Contagion = null;
    static private Material Mat_Player = null;

    GameObject _visualCube = null;

    GameLogic.Owner _currentOwner = GameLogic.Owner.Nuetral;
    public GameLogic.Owner Owner { get { return _currentOwner; } }

    static public void CreateMaterials()
    {
        Shader_Default = Shader.Find("Self-Illumin/Diffuse");

        // Nuetral material.
        Mat_Nuetral = new Material(Shader_Default);
        Mat_Nuetral.color = Color.gray;

        // Contagion material.
        Mat_Contagion = new Material(Shader_Default);
        Mat_Contagion.color = Color.green;

        // Player material.
        Mat_Player = new Material(Shader_Default);
        Mat_Player.color = Color.blue;
    }

    public void Initialize( int x, int y )
    {
        _visualCube = GameObject.CreatePrimitive(PrimitiveType.Quad);
        _visualCube.transform.parent = this.transform;
		Destroy(_visualCube.collider);
		gameObject.AddComponent<BoxCollider2D>();

        transform.position = new Vector3(0.5F + x + (x * 0.1F), 0.5F + y + (y * 0.1F), 0);

        SetOwner(GameLogic.Owner.Nuetral);
    }

    public void SetOwner(GameLogic.Owner newOwner)
    {
        _currentOwner = newOwner;

        _visualCube.renderer.material = GetMaterialByOwner(_currentOwner);
    }

    private Material GetMaterialByOwner(GameLogic.Owner owner)
    {
        Material material;

        switch (owner)
        {
            case GameLogic.Owner.Contagion:
                {
                    material = Mat_Contagion;
                }
                break;
            case GameLogic.Owner.Player:
                {
                    material = Mat_Player;
                }
                break;
            case GameLogic.Owner.Nuetral:
            default:
                {
                    material = Mat_Nuetral;
                }
                break;
        }

        return material;
    }
}