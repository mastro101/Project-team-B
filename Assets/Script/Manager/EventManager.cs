using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour
{

    GamePlayManager GPM;

    private void Start()
    {
        GPM = FindObjectOfType<GamePlayManager>();
    }


    public void Heal (int _life, Player _player1, Player _player2 = null, Player _player3 = null)
    {
        _player1.Life += _life;
        if (_player2 != null && _player3 != null)
        {
            _player2.Life += _life;
            _player3.Life += _life;
        }
    }

    public void TakeCredits(int _credits, Player _player1, Player _player2 = null, Player _player3 = null)
    {
        _player1.Credit += _credits;
        if (_player2 != null && _player3 != null)
        {
            _player2.Credit += _credits;
            _player3.Credit += _credits;
        }
    }

    public void AddMaterial(int materialType, int _material, Player _player1, Player _player2 = null, Player _player3 = null)
    {
        _player1.Materiali[materialType] += _material;
        if (_player1.Materiali[materialType] < 0)
            _player1.Materiali[materialType] = 0;
        if (_player2 != null && _player3 != null)
        {
            _player2.Materiali[materialType] += _material;
            if (_player2.Materiali[materialType] < 0)
                _player2.Materiali[materialType] = 0;

            _player3.Materiali[materialType] += _material;
            if (_player3.Materiali[materialType] < 0)
                _player3.Materiali[materialType] = 0;
        }
    }

    public void AddRandomMaterial(int _materiali, Player _player1, Player _player2 = null, Player _player3 = null)
    {
        for (int i = 0; i < _materiali; i++)
        {
            AddMaterial(Random.Range(0, 4), 1, _player1);
            if (_player2 != null && _player3 != null)
            {
                AddMaterial(Random.Range(0, 4), 1, _player2);
                AddMaterial(Random.Range(0, 4), 1, _player3);
            }
        }
    }

    public void RemoveRandomMaterial(int _materiali, Player _player1, Player _player2 = null, Player _player3 = null)
    {
        int m;
        for (int i = 0; i < _materiali; i++)
        {
            if (_player1.Materiali[0] + _player1.Materiali[1] + _player1.Materiali[2] + _player1.Materiali[3] > 0)
            {
                do
                {
                    m = Random.Range(0, 4);
                }
                while (_player1.Materiali[m] == 0);
                AddMaterial(m, -1, _player1);
            }
            if (_player2 != null && _player3 != null)
            {
                if (_player2.Materiali[0] + _player2.Materiali[1] + _player2.Materiali[2] + _player2.Materiali[3] > 0)
                {
                    do
                    {
                        m = Random.Range(0, 4);
                    }
                    while (_player2.Materiali[m] == 0);
                    AddMaterial(m, -1, _player2);
                }

                if (_player3.Materiali[0] + _player3.Materiali[1] + _player3.Materiali[2] + _player3.Materiali[3] > 0)
                {
                    do
                    {
                        m = Random.Range(0, 4);
                    }
                    while (_player3.Materiali[m] == 0);
                    AddMaterial(m, -1, _player3);
                }
            }
        }

        
    }


    public void PayForMaterial(int _credit, int _materialType, int _materiali, Player _player1)
    {
        if (_player1.Credit >= _credit)
        {
            AddMaterial(_materialType, _materiali, _player1);
            _player1.Credit -= _credit;
        }
        else
        {
            _player1.Lg.SetTextLog("You don't have enough Credits", true);
        }

    }

    public void PayForRandomMaterial(int _credit, int _materiali, Player _player1)
    {
        if (_player1.Credit >= _credit)
        {
            AddMaterial(Random.Range(0, 4), _materiali, _player1);
            _player1.Credit -= _credit;
        }
        else
        {
            _player1.Lg.SetTextLog("You don't have enough Credits", true);
        }

    }


    public void Jynix(Player _player, int _materiali, int possibility, int _credit, bool allIn = false)
    {
        int random = Random.Range(1, 11);
        int chooseMateriali;
        if (_player.Materiali[0] + _player.Materiali[1] + _player.Materiali[2] + _player.Materiali[3] >= _materiali)
        {
            if (allIn)
            {
                do
                {
                    chooseMateriali = Random.Range(0, 4);                    
                }
                while (_player.Materiali[chooseMateriali] == 0);
                _player.Materiali[chooseMateriali] = 0;
                if (random <= possibility)
                {
                    _player.WinPoint++;
                    _player.Lg.SetTextLog("Hai vinto un punto Vittoria", true);
                }
            }
            else
            {
                for (int i = 0; i < _materiali; i++)
                {
                    do
                    {
                        chooseMateriali = Random.Range(0, 4);
                    }
                    while (_player.Materiali[chooseMateriali] == 0);

                    if (_player.Materiali[chooseMateriali] != 0)
                        _player.Materiali[chooseMateriali]--;
                }

                if (random <= possibility)
                {
                    _player.Credit += _credit;
                    _player.Lg.SetTextLog("You win " + _credit + " credits", true);
                }
                else
                {
                    _player.Lg.SetTextLog("You lost the bet", true);
                }
            }
        }
        else
        {
            _player.Lg.SetTextLog("You don't have enough materials", true);
        }
    }
}
