using System;
using System.Windows.Forms;
using COMP3401_Project.ECSPackage.Delegates;
using COMP3401_Project.ECSPackage.Delegates.Interfaces;

namespace COMP3401_Project.PongPackage.Forms
{
    /// <summary>
    /// Partial Class which contains logic related to creating a specified number of entities to be displayed on screen
    /// Author: William Smith
    /// Date: 27/03/22
    /// </summary>
    public partial class CreationScreen : Form, IInitialiseCreateMultiDel, IInitialiseDeleteDel
    {
        #region FIELD VARIABLES

        // DECLARE a CreateMultipleDelegate, name it '_create':
        private CreateMultipleDelegate _create;

        // DECLARE a DeleteDelegate, name it '_delete':
        private DeleteDelegate _delete;

        // DECLARE an int, name it '_entityCount':
        private int _entityCount;

        #endregion


        #region CONSTRUCTOR

        /// <summary>
        /// Constructor for objects of CreationScreen
        /// </summary>
        public CreationScreen()
        {
            // CALL InitializeComponent():
            InitializeComponent();
        }

        #endregion


        #region IMPLEMENTATION OF IINITIALISECREATEMULTIDEL

        /// <summary>
        /// Initialises an object with a 'CreateMultipleDelegate' method
        /// </summary>
        /// <param name="pCreateMultiDel"> Create Multiple Method </param>
        public void Initialise(CreateMultipleDelegate pCreateMultiDel)
        {
            // INITIALISE _create with reference to pCreateMultiDel:
            _create = pCreateMultiDel;
        }

        #endregion


        #region IMPLEMENTATION OF IINITIALISEDELETEDEL

        /// <summary>
        /// Initialises an object with a 'DeleteMultipleDelegate' method
        /// </summary>
        /// <param name="pDeleteDel"> Delete Multiple Method </param>
        public void Initialise(DeleteDelegate pDeleteDel)
        {
            // INITIALISE _delete with reference to pDeleteDel:
            _delete = pDeleteDel;
        }

        #endregion


        #region EVENTS

        /// <summary>
        /// Event which is called when User clicks 'Create' button
        /// </summary>
        /// <param name="sender"> Object calling this method </param>
        /// <param name="e"> Necessary arguments to complete behaviour </param>
        private void CreateBttn_Click(object sender, EventArgs e)
        {
            // IF _entityCount is greater than 0, prevents having a negative number and zero entities being created:
            if (_entityCount > 0)
            {
                // CALL _create delegate, passing _entityCount as a parameter:
                _create(_entityCount);
            }
        }

        /// <summary>
        /// Event which is called when number value has changed
        /// </summary>
        /// <param name="sender"> Object calling this method </param>
        /// <param name="e"> Necessary arguments to complete behaviour </param>
        private void EntityCount_ValueChanged(object sender, EventArgs e)
        {
            // SET value of _entityCount to same as EntityCount.Value:
            _entityCount = (int)EntityCount.Value;
        }

        private void TerminateBttn_Click(object sender, EventArgs e)
        {
            // IF _entityCount is greater than 0, prevents having a negative number and zero entities being deleted:
            if (_entityCount > 0)
            {
                // CALL _delete delegate, passing _entityCount as a parameter:
                _delete(_entityCount);
            }
        }


        #endregion
    }
}
