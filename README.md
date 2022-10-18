# 專輯購物網站
## 簡介： 
網站設計可分為會員未登入和登入後兩種情境，下圖為登入前後介面操作的介紹。在網站設計中使用到了新增、刪除、修改、查詢四大功能。  
![image](https://user-images.githubusercontent.com/96182969/196511383-0e67a0c4-4063-47e1-877d-8bf56c868a5b.png)  
##  
![image](https://user-images.githubusercontent.com/96182969/196511415-6a720dda-8414-453e-82dc-4fdee610e459.png)  

## 功能說明：
本系統提供購買唱片的服務，包括註冊會員及登入、加入購物車、編輯收件人地址以及查看訂單明細，若無登入會員則會跳入到登入頁面，登入後才能進行選購。  
## 資料需求說明： 
● 會員(Member) - 包含會員id、會員密碼、會員姓名、會員email   
● 商品(Product) - 包含商品編號、商品名稱、商品價格、商品分類、歌手名稱、商品 圖片  
● 訂單(Order) - 包含訂單編號、會員id、會員姓名、會員email、收件人地址、收件日期  
● 訂單明細(Record) - 包含訂單編號、會員id、商品編號、商品名稱、商品價格、商品數量、是否為訂單  
## ERD
![image](https://user-images.githubusercontent.com/96182969/196511602-26a7f422-f52c-41b7-a321-3f35d7838006.png)  
## Relational Schema
![image](https://user-images.githubusercontent.com/96182969/196511660-ec240b7d-85e8-4749-b451-f886d3503d1c.png)  
## Relational Schema對應的SQL語法
CREATE TABLE Member (  
mId	NVARCHAR (50) NOT NULL,   
mPwd NVARCHAR (100) NULL,   
mName NVARCHAR (50) NULL,   
mEmail NVARCHAR (50) NULL,   
PRIMARY KEY (mId)  
);  
CREATE TABLE "Order" (  
oId	NVARCHAR (50) NOT NULL,   
mId		NVARCHAR (50) NULL,   
mName		NVARCHAR (50) NULL,   
mEmail NVARCHAR (50) NULL,   
oAddress NVARCHAR (50) NULL,   
oDate	DATETIME	NULL,   
PRIMARY KEY (oId),  
    FOREIGN KEY(mId)REFERENCES Member(mId)  
);  
CREATE TABLE Product (  
pId	NVARCHAR (50) NOT NULL,   
pName		NVARCHAR (100) NULL,  
pPrice	INT	NULL,   
pCategory NVARCHAR (50) NULL,  
pSinger NVARCHAR (50) NULL, 
Pimg	NVARCHAR (50) NULL,   
PRIMARY KEY (pId)  
);  
CREATE TABLE Records (  
oId	NVARCHAR (50) NOT NULL,  
mId		NVARCHAR (50) NOT NULL,   
pId	NVARCHAR (50) NULL,   
pName			NVARCHAR (50) NULL,  
pPrice	INT	NULL,  
oNum	INT	NULL,  
oIsApproved NVARCHAR (50) NULL,   
PRIMARY KEY (oId, mId),  
   FOREIGN KEY(oId)REFERENCES "Order"(oId), 
   FOREIGN KEY (mId) REFERENCES Member(mId),   
   FOREIGN KEY (pId) REFERENCES Product(pId));  

## 資料庫圖表
![image](https://user-images.githubusercontent.com/96182969/196512727-3f5ba989-2f38-4552-b15a-df0d892045e3.png)  
## 實際前端網頁系統
網站首頁，顯示產品列表及登入(測試用帳密皆為123)和註冊會員的功能。  
 ![image](https://user-images.githubusercontent.com/96182969/196512921-f7cd1012-7b0e-4098-b221-834446867277.png)  
若未先登入會員點選商品加入購物車會自動跳到登入會員的網頁進行登入。  
![image](https://user-images.githubusercontent.com/96182969/196512967-9919f386-0fb0-468b-8624-516c2b46e4dc.png)  
登入後最上方列表會改變顏色，標題變成「線上購物商城-會員專區」，且新增購物車、訂單的選項，登入鍵變更成登出鍵，最後會出現帳號名稱加上歡迎光臨。  
 ![image](https://user-images.githubusercontent.com/96182969/196512994-213759cc-bd8d-4678-a971-55e3bea1de9c.png)  
將所需產品加入購物車後，到購物車清單查看購物車內容，會出現購物車內產品的各項資訊，也可以進行刪除該產品的動作。  
![image](https://user-images.githubusercontent.com/96182969/196513034-86995231-b114-4aca-9033-821313299f20.png)  
填寫訂單收件人相關資料後按下確認鍵，則會將收件人相關資料與確認訂購之日期時間放到訂單檔(Order)，並將購物車的商品放到訂單明細(Record) 紀錄。  
 ![image](https://user-images.githubusercontent.com/96182969/196513053-953f89e8-e621-43f5-9444-9bc47373852f.png)  
完成確認訂購後，將自動跳轉到訂單查詢的頁面並看到前一步驟輸入的收件人資料，或按下導覽列的“訂單”連結，也可到達此頁面。此頁面目的為顯示目前會員的訂單檔(Order)。  
![image](https://user-images.githubusercontent.com/96182969/196513096-351031e8-23d0-4773-9c68-a25745f34f3c.png)  
按下確認訂購後，查看按右方查看訂單明細，按下後出現以下畫面  
 ![image](https://user-images.githubusercontent.com/96182969/196513127-0730a3fa-49c6-4a9f-8345-32a548f2aa16.png)  
也可按編輯訂單，更改姓名、信箱以及地址  
![image](https://user-images.githubusercontent.com/96182969/196513161-8f411468-8e05-4bce-be7d-d282d7ef61fc.png)  
例如將地址改為高雄市，按下儲存鍵後會自動回訂單列表，可看到收件人地址成功從原本的台南市變為高雄市  
![image](https://user-images.githubusercontent.com/96182969/196513179-c4bf0b57-ebae-44d2-afdc-2b76f049f91d.png)  

## 額外補充說明 
起初用IIS操作，但嘗試多次後仍然失敗，所以最後只能使用Visaul studio 執行。   
(下圖為IIS操作失敗畫面)  
![image](https://user-images.githubusercontent.com/96182969/196513282-32047218-0328-423d-8495-c5deb1841611.png)  
![image](https://user-images.githubusercontent.com/96182969/196513307-15fe4f01-cecb-49f8-8e18-d096b7b7e00a.png)  
![image](https://user-images.githubusercontent.com/96182969/196513332-2fea3684-f6ff-43e0-a0da-29da32ce9555.png)  
![image](https://user-images.githubusercontent.com/96182969/196513346-f67edf46-f44f-4342-8541-cff65c4a8e9d.png)  

 
