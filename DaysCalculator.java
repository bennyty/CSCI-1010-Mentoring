import java.util.*;

public class DaysCalculator {
    public static void main(String[] args) {
        List<Double> giftCosts = Arrays.asList(100.00, 2.50, 53.42, 10.50, 88.00, 16.99, 7.99, 101.52, 200.00, 21.99, 1.11, 2.15);
        // List<Double> giftCosts = Arrays.asList(104.99, 20.00, 15.00, 99.96, 65.00, 50.00, 600.00, 5.15, 508.46, 403.91, 186.65, 185.36);
        // List<Double> giftCosts = Arrays.asList(104.99, 18.00, 25.00, 45.96, 125.00, 61.00, 600.00, 5.15, 508.46, 403.91, 146.65, 185.36);

        double runningTotal = 0;
        for (int i = 0; i < 12; ++i) {
            runningTotal += (12-i) * (i+1) * giftCosts.get(i);
        }
        System.out.println(runningTotal);
    }
}


