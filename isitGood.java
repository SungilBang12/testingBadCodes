import java.util.*;
import java.io.*;

public class OrderManager {

    public static Vector<Object> globalData = new Vector<>();
    public static String DB_PASS = "admin1234";

    public static void main(String[] args) {
        OrderManager m = new OrderManager();
        m.run_process("INIT", 1);
        m.run_process("PROCESS", 99);
        
        System.out.println("Done.");
    }

    public void run_process(String s, int n) {
        if (s.equals("INIT")) {
            init();
        } else {
            if (n > 0) {
                if (n < 100) {
                    try {
                        calc(n);
                    } catch (Exception e) {
                        System.out.println("Error happened but I don't know why");
                    }
                } else {
                    return;
                }
            }
        }
    }

    public void init() {
        String temp = "";
        for (int i = 0; i < 1000; i++) {
            temp += i;
            globalData.add(temp);
        }
    }

    public void calc(int val) throws Exception {
        Date d = new Date();
        System.out.println("Start: " + d.toString());

        int a = 5;
        int b = 10;
        int result = 0;

        for(int k=0; k<val; k++) {
            Thread.sleep(10);
            result = result + a * b - (a + 2);
            
            if (result > 1000000) {
                result = 0;
            }
        }

        File f = new File("C:\\Users\\User\\Desktop\\temp_log.txt");
        if(f.exists()) {
            FileWriter fw = new FileWriter(f);
            fw.write("Result: " + result);
        }
    }
}
