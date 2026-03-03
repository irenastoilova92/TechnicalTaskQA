Test Cases: Place a Bet on a Sports Event
Feature: Live Sports Betting
User Story: As a registered user, I want to place a bet on a live football match so that I can
potentially win money if my prediction is correct.
Test Author: Irena Stoilova

1. GENERAL ASSUMPTIONS
- Currency: EUR (€)
- Minimum stake: €X
- Maximum stake: €Y
- Session timeout: Z minutes of inactivity
- Potential winnings = Stake × Odds
- System validates all inputs before processing
- Authentication required for all betting operations
- Real-time balance checking is implemented

2. CRITICAL TEST CASES

1.TC-01: Successful Bet Placement with Valid Data

Priority: High
Preconditions:
- User is logged in.
- User has sufficient balance (ex. €100).
- At least one live football match is available.

Test Steps:
1. Navigate to Live Betting page.
2. Verify live football matches are displayed.
3. Select an available football match Y.
4. Select betting market XXX.
5. Click on a specific outcome ( for example – X – odd 2.5).
6. Verify bet slip opens with correct match details and odds.
7. Enter valid stake amount within balance (ex. €10)
8. Click "Confirm Bet" button.
9. Wait for bet confirmation.

Expected Result:
- Bet is successfully placed.
- Success confirmation message displayed.
- Bet appears in "My Bets" with correct details:
- Match name – Y.
- Selected outcome – X.
- Odds: 2.50
- Stake: €10
- Potential winnings: €25
- User balance reduced by stake (€100 → €90).
- User balance reduce is recorded in DB.

Test Data:
- Username: testTest123@example.com

2.TC-02: Bet Placement Blocked for Unauthenticated User

Priority: High
Preconditions:
- User is NOT logged in.
- Live football matches are available.

Test Steps:
1. Attempt to navigate to Live Betting page directly via URL.
2. Try accessing betting API endpoint without authentication token.

Expected Result:
- Redirect to Login page.
- Appropriate error message is shown.
- No access to betting functionality.

3. TC-03: Insufficient Balance Error Handling

Priority: High
Preconditions:
- User is logged in.
- User balance: €X.
- Live football match is available.

Test Steps:
1. Navigate to Live Betting page.
2. Select a live football match.
3. Select betting market and outcome with odds.
4. In bet slip, enter stake amount Y, Y>X.
5. Click "Confirm Bet" button.

Expected Result:
- Bet NOT placed.
- Error message is shown: "Insufficient balance. Please deposit to continue."
- Bet slip stays open for user to correct. /assumption/
- Balance is unchanged - €X.
- Server checks in DB user’s balance and updates client that user’s balance is insufficient.

4.TC-04: Bet Slip Displays Correct Odds and Calculations

Priority: High
Preconditions:
- User is logged in.
- User balance: €X.
- Live football match with odds available.

Test Steps:
1. Navigate to Live Betting page.
2. Select live football match.
3. Select betting market XXX.
4. Click on outcome Y with displayed odds Y1.
5. Verify bet slip shows odds: Y1.
6. Enter stake: €Z, Z<X.
7. Verify potential winnings calculation in bet slip.

Expected Result:
- Bet slip shows correct odds: Y1.
- Potential winnings are calculated: Stake × Odds = €Z.Y1
- Potential profit is shown: €Z.Y1 - €Z .
- Calculations update in real-time as user types / assumption/.
- There is new row in DB table for bets.

5. TC-05: Odds Change Before Bet Confirmation (Live Betting)

Priority: High
Preconditions:
- User is logged in.
- User balance: €X.
- Live football match in progress (odds can change frequently).

Test Steps:
1. Navigate to Live Betting page.
2. Select live football match.
3. Add outcome to bet slip with odds Y.
4. Enter stake.
5. Wait/simulate odds change to Z (before clicking Confirm).
6. Click "Confirm Bet" button.


Expected Result:
- System detects the odds change.
- Warning is displayed that odds have changed / assumption/.
- Bet NOT placed automatically with old odds.
- User can accept or reject new odds and proper outcome is expected / if accept – update
stakes and odd , if rejects – bet is rejected and there is no change of user’s balance/.

6. TC-06: Multiple Validation - Stake Amount Boundaries

Priority: High
Preconditions:
- User is logged in.
- User balance: €Z.
- Live football match is available.
- System has min/max stake limits - min €X, max €Y.

Test Steps:
Scenario A - Below Minimum:
1. Add outcome to bet slip.
2. Enter stake: €Z1, Z1<X.
3. Attempt to place bet.

Expected Result A:
- Error message: "Minimum stake is €X".
- Bet is not placed.

Scenario B - Exact Minimum:
1. Clear bet slip.
2. Enter stake: €X (exact minimum)
3. Place bet.

Expected Result B:
- Bet is successfully placed.

Scenario C - Above Maximum:
1. Clear bet slip
2. Enter stake: €Z2, Z2>Y.
3. Attempt to place bet.

Expected Result C:
- Error message: "Maximum stake is €Y"
- Bet is not placed.

Scenario D - Exact Maximum:
1. Clear bet slip
2. Enter stake: €Y (exact maximum)
3. Place bet.

Expected Result D:
- Bet is successfully placed.

7. TC-07: "My Bets" Section Display After Placement

Priority: Medium
Test Steps:
1. User is logged in and has balance.
1. Place a successful bet.
2. Navigate to "My Bets" section.
3. Verify bet details.

Expected Result:
- Bet shows up in "My Bets" right away .
- All bet details displayed:
- Match name and date/time
- Market and outcome selected
- Odds at placement time
- Stake amount
- Potential winnings
- Status (Pending/Open)
- When the bet was placed.
- There is record in DB for the placed bet with all corresponding info.

3. ADDITIONAL TEST CASES

8. TC-08: Bet Placement After Session Timeout

Priority: Medium
Test Steps:
1. User logs in and adds bet to slip.
2. Wait for session timeout (e.g., Z minutes of inactivity)
3. Attempt to place bet.

Expected Result:
- Session timeout is detected.
- Redirect to login page with proper error message.
- Bet is NOT placed.

9. TC-09: Invalid Stake Input - Non-Numeric Characters

Priority: Medium
Test Steps:
1. Add outcome to bet slip.
2. In stake field, enter invalid data:
- Letters: "abc".
- Special characters: "€@#$".
- Empty field.
- Negative value: "-10".

Expected Result:
- Input validation blocks non-numeric entry OR error message is shown.
- Cannot place bet until valid stake entered.

10.TC-10: Concurrent Bet Placement - Race Condition

Priority: Minor
Test Steps:
1. User balance: €X.
2. Open two browser tabs with same user session.
3. In Tab 1: Add bet A with stake €Y, click Confirm.
4. Immediately in Tab 2: Add bet B with stake €Y, click Confirm.
5. Both requests sent nearly simultaneously.

Expected Result:
- First bet accepted (€Y deducted, new balance = €X-Y)
- Second bet rejected - error: "Insufficient balance" OR "Balance has changed"
- No double-spend happens.
- DB transaction locking works correctly.

11. TC-11: Bet on Suspended/Removed Market

Priority: Medium
Preconditions:
- User is logged in.
- Market is suspended/removed by admin or system.
Test Steps:
1. Add outcome to bet slip.
2. While bet slip is open, market is suspended.
3. Attempt to place bet

Expected Result:
- Error message is displayed.
- Bet is NOT placed.
- User can close bet slip and pick a different market.

12. TC-12: Special Characters and SQL Injection in Stake Field

Priority: Major
Preconditions:
- User is logged in.
- Bet slip is open.

Test Steps:
1. Add outcome to bet slip.
2. In stake field, enter SQL injection attempts:
- `10'; DROP TABLE bets;--`
- `10 OR 1=1`

Expected Result:
- Input sanitized/escaped properly.
- No SQL injection happens.
- Value treated as invalid or error shown.
- Database stays intact.

13. TC-13: Disconnect During Bet Placement

Priority: High
Preconditions:
- User is logged in.

Test Steps:
1. Add bet to slip and click Confirm.
2. User is disconnected due to technical reasons.
3. Reconnect network.
4. Check bet status and balance.

Expected Result:
- Bet is NOT placed.
- No duplicate bets if user retries.
- Balance updated correctly – only once if bet is placed.
- Bet status clearly shown (placed or failed).

14. TC-14: Betting on Two Different Markets

Priority: Medium
Preconditions:
- User is logged in.
- User balance: €X.
- Live football match is available with multiple betting markets.

Test Steps:
1. Navigate to Live Betting page.
2. Select a live football match.
3. Select market A and add outcome Y1 (odds 2.00) to bet slip.
4. Without placing the bet, select market B and add outcome Y2 (odds 1.80) to bet slip.
5. Verify bet slip shows both selections.
6. Enter stake €Z1 for first bet.
7. Enter stake €Z2 for second bet.
8. Click "Confirm Bets" button.

Expected Result:
- Both bets added to bet slip successfully.
- Bet slip shows corresponding info for both market bets.
- Both bets placed successfully.
- User balance reduced by total stake (€X-Z1-Z2)
- Both bets appear in "My Bets" section with correct details.
- In DB both bet records are displayed.

Additional tests to cover / there are no requirements provided, so I just suggest them/

- TC-15: Bet slip expires after X minutes.
- TC-16: Auto-refresh odds functionality.
- TC-17: Countdown timer display.
- TC-18: Attempt to bet after match ended.
- TC-19: Verify max payout validation.
- TC-20: Calculate max allowed stake based on odds and payout limit.
- TC-21: Decimal precision validation.
- TC-22: Currency format handling.