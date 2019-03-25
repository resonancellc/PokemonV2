using Pokemon.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokemon
{
    public partial class ItemForm : Form
    {
        //Form parent;
        IPokemonParty<IPokemon> _pokemonParty;
        IEquipment _equipment;
        BattleForm _battleForm;

        public ItemForm(IEquipment equipment, IPokemonParty<IPokemon> pokemonParty, BattleForm battleForm)
        {
            InitializeComponent();
            //if (isShopForm)
            //    this.parent = parent as AfterWinForm;
            //else
            //    this.parent = parent as BattleForm;
            _equipment = equipment;
            _battleForm = battleForm;
            _pokemonParty = pokemonParty;
            this.Text = "Items";
            //this.Text = isShopForm ? "Shop" : "Items";
            int offset = 0;
            foreach (KeyValuePair<IEquipmentItem,int> item in equipment.EquipmentList)
            {
                // quantity = item.Value ;
                ItemPanel itemPanel = new ItemPanel(item.Key, item.Value, this);
                itemPanel.Location = new Point(0, offset);
                this.Controls.Add(itemPanel);
                offset += itemPanel.Size.Height;
            }
        }



        private void ItemForm_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //if (parent is AfterWinForm) ((AfterWinForm)parent).RefreshBalance();

            StaticMain.FormClosed(this);
        }

        public void ItemPicked(int id)
        {
            BattleLog.ClearText();
            bool itemUsed = false;
            if (id == 4)
            {
                IPokemon pokemon = _pokemonParty.ActivePokemon;
                if (ItemHelper.CanUseItem(pokemon, id))
                {
                    ItemHelper.UseItem(pokemon, id);
                    _equipment.UseItem(id);
                    itemUsed = true;
                    BattleLog.AppendText($"Used {ItemHelper.GetItemNameByID(id)} on {pokemon.Name}!");
                } 
                else
                {
                    BattleLog.AppendText($"It is not the right moment to use this");
                }
            } 
            else
            {
                PokemonPartyForm pokemonPartyForm = new PokemonPartyForm(_pokemonParty);
                pokemonPartyForm.BringToFront();

                if (pokemonPartyForm.ShowDialog() == DialogResult.OK)
                {
                    IPokemon pokemon = pokemonPartyForm.PickedPokemon;
                    if (ItemHelper.CanUseItem(pokemon, id))
                    {
                        ItemHelper.UseItem(pokemon, id);
                        _equipment.UseItem(id);
                        itemUsed = true;
                        BattleLog.AppendText($"Used {ItemHelper.GetItemNameByID(id)} on {pokemon.Name}!");
                    }
                    else
                    {
                        BattleLog.AppendText($"It is not the right moment to use this");
                    }
                }
            }

            this.Close();
            _battleForm.AfterItemPickAction(itemUsed);
        }
    }
}


//foreach (EquipmentItem item in ItemsList.Items.Values)
//            {
//                if (item != null)
//                {
//                    ItemPanel itemPanel = null;
//itemPanel = parent is BattleForm? new ItemPanel(item, isShopForm, this, parent as BattleForm) : new ItemPanel(item, isShopForm, this);
//itemPanel.Location = new Point(0, offset);

//                    this.Controls.Add(itemPanel);
//                    offset += itemPanel.Size.Height;
//                }
//            }