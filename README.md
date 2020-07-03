# OceanStar
# File: PromotionEngineTestSet.cs
# Unit price for SKU IDs 
A 50 
B 30 
C 20
D 15

Active Promotions 
3 of A's for 130 
2 of B's for 45 
C & D for 30

# Test Name: GetCartTotal_With_No_Promotion_Test()
# Scenario A 
1 * A 50 
1 * B 30 
1 * C 20 
====== 
Total 100

# Test Name: Cart_With_MulipleTypeSkus_FlatPrice_PromotionTest()
# Scenario B
5 * A 
5 * B 
1 * C 
====== 
Total 370

# Cart_With_MulipleTypeSkus_Combo_FlatPrice_PromotionTest()
# Scenario C
3* A
5* B
1* C
1* D
======
Total 280
