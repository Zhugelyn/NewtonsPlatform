using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paint : MonoBehaviour
{
    [SerializeField] private Texture2D _texture;
    [SerializeField] private Collider _collider;
    [SerializeField] private int _textureSize = 128;
    [SerializeField] private int _brushSize = 4;
    [SerializeField] private Color _color;
    [SerializeField] private float lastX;
    [SerializeField] private float lastY;
    [SerializeField] private Camera _camera;

    private void Start()
    {
        _texture = new Texture2D(_textureSize, _textureSize);
    }

    private void Update()
    {
        Draw();
        if (Input.GetMouseButton(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (_collider.Raycast(ray, out hit, 100f))
            {
                lastX = (int)(hit.textureCoord.x * _textureSize);
                lastY = (int)(hit.textureCoord.y * _textureSize);
            }
        }
    }
    private void Draw()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (_collider.Raycast(ray, out hit, 100f))
        {
            int rayX = (int)(hit.textureCoord.x * _textureSize);
            int rayY = (int)(hit.textureCoord.y * _textureSize);
            MiddleDrow(lastX, lastY, rayX, rayY);
            lastX = rayX;
            lastY = rayY;
            //DrowQuad(rayX, rayY);
            DrawCircle(rayX, rayY);

            _texture.Apply();
        }
    }

    private void MiddleDrow(float X1, float Y1, float X2, float Y2)
    {
        float Asq = Mathf.Pow(X1 - X2, 2f);
        float Bsq = Mathf.Pow(Y1 - Y2, 2f);
        float Croot = Mathf.Pow(Asq + Bsq, 0.5f);


        if (Croot > _brushSize / 4)
        {
            float Cx = (X1 + X2) / 2;
            float Cy = (Y1 + Y2) / 2;

            DrawCircle((int)Cx, (int)Cy);
            MiddleDrow((int)X1, (int)Y1, (int)Cx, (int)Cy);
            MiddleDrow((int)Cx, (int)Cy, (int)X2, (int)Y2);
        }
    }

    private void OnMouseDown()
    {

    }

    private void DrawCircle(int rayX, int rayY)
    {
        for (int y = 0; y < _brushSize; y++)
            for (int x = 0; x < _brushSize; x++)
            {
                float x2 = Mathf.Pow(x - _brushSize / 2, 2);
                float y2 = Mathf.Pow(y - _brushSize / 2, 2);
                float r2 = Mathf.Pow(_brushSize / 2, 2);

                if (x2 + y2 < r2)
                    _texture.SetPixel(rayX + x - _brushSize / 2, rayY + y - _brushSize / 2, _color);
            }
    }
}
