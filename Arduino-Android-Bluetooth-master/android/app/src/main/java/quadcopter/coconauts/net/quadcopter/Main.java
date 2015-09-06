package quadcopter.coconauts.net.quadcopter;

import android.bluetooth.BluetoothAdapter;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.support.v7.app.ActionBarActivity;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.SeekBar;
import android.widget.TextView;
import android.widget.Toast;


public class Main extends ActionBarActivity {

    public final String TAG = "Main";

    private SeekBar elevation;
    private TextView debug;
    private TextView status;
    private Bluetooth bt;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        debug = (TextView) findViewById(R.id.textDebug);
        status = (TextView) findViewById(R.id.textStatus);

        findViewById(R.id.restart).setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                connectService();
            }
        });

        elevation = (SeekBar) findViewById(R.id.seekBar);
        elevation.setOnSeekBarChangeListener(new SeekBar.OnSeekBarChangeListener() {

            @Override
            public void onStopTrackingTouch(SeekBar seekBar) {
                Log.d("Seekbar", "onStopTrackingTouch ");
                int progress = seekBar.getProgress();
                String p = String.valueOf(progress);
                debug.setText(p);
                Toast.makeText(getBaseContext(), "Message - " + p.toString(), Toast.LENGTH_LONG).show();
                bt.sendMessage(p, getBaseContext());

            }

            @Override
            public void onStartTrackingTouch(SeekBar seekBar) {
                Log.d("Seekbar", "onStartTrackingTouch ");
            }

            @Override
            public void onProgressChanged(SeekBar seekBar, int progress, boolean fromUser) {
                //Log.d("Seekbar", "onProgressChanged " + progress);
            }
        });

        bt = new Bluetooth(this, mHandler);
        connectService();
        Toast.makeText(getBaseContext(), "Create Complete", Toast.LENGTH_LONG).show();
    }

    public void connectService() {
        try {

            Toast.makeText(getBaseContext(), "Connection Start", Toast.LENGTH_LONG).show();
            status.setText("Connecting...");
            BluetoothAdapter bluetoothAdapter = BluetoothAdapter.getDefaultAdapter();
            if (bluetoothAdapter.isEnabled()) {
                bt.start();
                bt.connectDevice("HC-05");
                Toast.makeText(getBaseContext(), "Status -" + bt.getState(), Toast.LENGTH_LONG).show();
                Log.d(TAG, "Btservice started - listening");
                status.setText("Connected");
            } else {
                Toast.makeText(getBaseContext(), "bluetooth is not enabled", Toast.LENGTH_LONG).show();
                Log.w(TAG, "Btservice started - bluetooth is not enabled");
                status.setText("Bluetooth Not enabled");
            }
        } catch (Exception e) {
            Toast.makeText(getBaseContext(), "Error in connection - " + e.getMessage(), Toast.LENGTH_LONG).show();
            Log.e(TAG, "Unable to start bt ", e);
            status.setText("Unable to connect " + e);
        }
    }

    private String msgBuffer = "";

    private void DisplayMessage(String message) {
        try {
            if (message.contains("|")) {
                String[] parts = message.split("|");
                //Write logic to send to API
                if (!parts[parts.length - 1].contains("|")) {
                    msgBuffer = parts[parts.length - 1];
                } else {
                    msgBuffer="";
                }
                Toast.makeText(getBaseContext(), "First : "+parts[0]+"Last : " +parts[parts.length - 1] , Toast.LENGTH_LONG).show();
            }

        } catch (Exception e) {
            Toast.makeText(getBaseContext(), "Error in connection - " + e.getMessage(), Toast.LENGTH_LONG).show();
        }
    }

    private final Handler mHandler = new Handler() {
        @Override
        public void handleMessage(Message msg) {
            switch (msg.what) {
                case Bluetooth.MESSAGE_STATE_CHANGE:
                    Log.d(TAG, "MESSAGE_STATE_CHANGE: " + msg.arg1);
                    break;
                case Bluetooth.MESSAGE_WRITE:
                    Log.d(TAG, "MESSAGE_WRITE ");
                    break;
                case Bluetooth.MESSAGE_READ:
                    //DisplayMessage(msg.obj.toString());
                    //Toast.makeText(getBaseContext(), "Message from Arduino - " + msg.obj, Toast.LENGTH_LONG).show();
                    Log.d(TAG, "MESSAGE_READ ");
                    break;
                case Bluetooth.MESSAGE_DEVICE_NAME:
                    Log.d(TAG, "MESSAGE_DEVICE_NAME " + msg);
                    break;
                case Bluetooth.MESSAGE_TOAST:
                    Log.d(TAG, "MESSAGE_TOAST " + msg);
                    break;
            }
        }
    };

}
