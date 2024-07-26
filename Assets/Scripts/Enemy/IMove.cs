using UnityEngine;

namespace Main
{
    //��������� ��� ���������������� � ������������, ����������� �� ������������
    public interface IMove
    {
        void SetVelocity(Vector2 direction, float speed);
        void SetVelocityMultiplier(float multiplier);
    }
}