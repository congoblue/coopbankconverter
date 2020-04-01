# coopbankconverter
Converter to make coop bank statement csv export work with Quickbooks

See CoopBankConverter/publish/setup.exe for Windows installer

Usage: 
- Export CSV file from Coop making sure it goes back far enough to include all transactions
- Drag & drop CSV file onto the text window on the converter app
- Click Export button. Will create file "qb.csv" on the desktop
- Import this into QB
- Also creates "QbBankHistory.csv" on the desktop which holds previously imported transactions for checking (bit hacky sorry)
