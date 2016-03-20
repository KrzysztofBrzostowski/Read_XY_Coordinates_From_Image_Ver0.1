using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ReadXYCoordinatesFromImage.Utilities
{
    public class CommandBase : ICommand
    {
        private Action<Object> m_Action;
        private Func<Object, Boolean> m_Functor;

        public CommandBase(Action<Object> _action, Func<Object, Boolean> _func)
        {
            m_Action = _action;
            m_Functor = _func;
        }

        public bool CanExecute(object parameter)
        {
            if (m_Functor != null)
                return m_Functor(parameter);
            else
                return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            m_Action(parameter);
        }


    }
}
