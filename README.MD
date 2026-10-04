1. Singleton

El TouchInputManager es un monobehaviour singleton que maneja el cambio de tipo de input en los controles touch (entre el tipo de input horizontal para manejar el camion y el swipe para la descarga)
Tenerlo como singleton asegura que desde solo ese lugar se realiza el cambio, por lo tanto puede ser llamado desde cualquier lado del codigo. Aparte de esto, tiene UN RegisterInputs, desde el cual los jugadores se Registran a ellos mismos.