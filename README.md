# OTA Two-way Service Bus Sample
The ATP Two-Way CMS Interface Exchange will enable the court
to subscribe and consume the ATP data, asynchronously, via a
standard electronic interface/API.

---
## Getting Started

### Sample Projects
We have some sample projects to get you started in these formats:
* [Dotnet Framework 4.7.2](DotnetFramework/README.md)
* [Dotnet Core 3.1](DotnetCore/README.md)
* [Dotnet 6](Dotnet6/README.md)
* [Dotnet 7](Dotnet7/README.md)
* [NodeJS with JavaScript](NodeJS-JavaScript/README.md)
* [NodeJS with TypeScript](NodeJS-TypeScript/README.md)

### Configuration Settings
You will need to replace configuration settings provided to you by the JCC Project Manager if you connect to a Topic:
* TenantId
* ClientId
* ClientSecret
* ServiceBusNamespace
* TopicName
* SubscriptionName
You will need to replace configuration settings provided to you by the JCC Project Manager if you connect to a Queue:
* ServiceBusNamespace
* QueueName
* SharedAccessKey
* SharedAccessKeyName
---
## Case Action

### Trigger Actions

The following actions in the ATP tool will trigger the Case Action message:
- [Petition is submitted by the Defendant](#petition-is-submitted-by-the-defendant)
- [Petition is approved by Judicial Officer](#petition-is-approved-by-judicial-officer)
- [Petition is denied by Judicial Officer](#petition-is-denied-by-judicial-officer)
- [Order is served by the Court Clerk](#order-is-served-by-the-court-clerk)
- [Order is vacated by the Court Administrator](#order-is-vacated-by-the-court-administrator)

### The schema for the ATP Case Action message (Topic)

#### The Case Action Message has fields of type String, DateTime, Number and Boolean. They can all contain null values.

| Name | Type | Description |
| :--- | :---: | :--- |
| `requestId` | String | Unique identifier for the request |
| `caseNumber` | String | Case Number |
| `citationNumber` | String | Citation Number |
| `county` | String | County |
| `triggerAction` | String | Action that triggered the message.<br>Valid values are:<ul><li>"`Submitted`"</li><li>"`Approved`"</li><li>"`Rejected`"</li><li>"`Order Served`"</li><li>"`Vacated`"</li></ul> |
| `petitioner` | String | Petitioner |
| `petitionerPhoneNumber` | String | Petitioner’s phone number |
| `petitionerEmail` | String | Petitioner’s email address |
| `petitionSubmittedOn` | DatTime | Date the petition was submitted on |
| `judgment` | String | Judgment<br>Valid values are:<ul><li>"`Approved`"</li><li>"`Rejected`"</li></ul> |
| `judgmentBy` | String | Judgment By |
| `judgmentByRole` | String | Judgment By Role<br>Valid values are:<ul><li>"`Court Clerk`"</li><li>"`Judicial Officer`"</li></ul> |
| `judgmentDate` | DateTime | Date the judgment was made on |
| `denialReason` | String | Reason for denying the petition |
| `rejectionDate` | String | Date the petition was rejected on |
| `reducedAmountAccept` | Boolean | Indicates whether Reduction of Fines is accepted |
| `reducedAmountDeny` | Boolean | Indicates whether Reduction of Fines is denied |
| `finalReducedAmount` | Number | Final Reduced Amount |
| `paymentPlanAccept` | Boolean | Indicates whether Payment Plan is accepted |
| `paymentPlanDeny` | Boolean | Indicates whether Payment Plan is denied |
| `finalMonthlyPaymentAmount` | Number | Final Monthly Payment Amount |
| `monthlyPaymentDate` | DateTime | Monthly Payment Date |
| `moreTimeAccept` | Boolean | Indicates whether More Time to Pay is accepted |
| `moreTimeDeny` | Boolean | Indicates whether More Time to Pay is denied |
| `moreTimeToPayAmount` | Number | More Time to Pay Amount |
| `moreTimeToPayByDate` | DateTime | More Time to Pay by Date |
| `communityServiceAccept` | Boolean | Indicates whether Community Service is accepted |
| `communityServiceDeny` | Boolean | Indicates whether Community Service is denied |
| `judgeOrderedCommunityServiceHours` | Number | Number of Community Service Hours Judge ordered |
| `judgeOrderedCommunityServiceDueDate` | DateTime | Due Date by which the defendant has to complete<br>the Community Service Hours |
| `orderServedBy` | String | Order Served By |
| `orderServedStatus` | Boolean | Indicates whether the Order was served or not |
| `orderServedDate` | DateTime | Date the Order was served |
| `plea` | String | Indicates what plea the Defendant took<br>Valid values are:<ul><li>"`Guilty`"</li><li>"`No Contest`"</li></ul> |
| `allRightsWaived` | Boolean | Indicates whether the Defendant waived all the rights |
| `totalDueAmount` | Number | Total Amount Due by the Defendant |
| `reducedAmountPayByDate` | DateTime | Date by when the Defendant has to pay the reduced amount |
| `recommendedReducedAmountPayByDate` | DateTime | ATP Tool recommended Date by when the Defendant<br>has to pay the reduced amount |
| `adjudicated` | Boolean | Indicates whether the case was adjudicated or not |
| `vacatedBy` | String | Vacated By |
| `vacatedDate` | DateTime | Date the Order was vacated |
| `vacatedReason` | String | Reason for vacating the order |
| `atpOrder` | String | Base64 format of the final ATP Order |

---

## Messages

### Petition is submitted by the Defendant

| Name | Type | Description |
| :--- | :---: | :--- |
| `requestId` | String | Unique identifier for the request |
| `caseNumber` | String | Case Number |
| `citationNumber` | String | Citation Number |
| `county` | String | County |
| `triggerAction` | String | Action that triggered the message<br>Valid value is "`Submitted`" |
| `petitioner` | String | Petitioner |
| `petitionerPhoneNumber` | String | Petitioner’s phone number |
| `petitionerEmail` | String | Petitioner’s email address |
| `petitionSubmittedOn` | DateTime | Date the petition was submitted on |

#### Sample Message:
```json
{
    "requestId" : "614e7eff8d5502c2f0d6336e",
    "caseNumber": "INF-19-C19037936",
    "citationNumber": "C19037936",
    "county": "San Francisco",
    "triggerAction": "Submitted",
    "petitioner": "John Doe",
    "petitionerPhoneNumber": "(213) 555-1212",
    "petitionerEmail": "john.doe@johndoe.com",
    "petitionSubmittedOn": "2019-01-01T03:59:15.561Z"
}
```

### Petition is approved by Judicial Officer

| Name | Type | Description |
| :--- | :---: | :--- |
| `requestId` | String | Unique identifier for the request |
| `caseNumber` | String | Case Number |
| `citationNumber` | String | Citation Number |
| `county` | String | County |
| `triggerAction` | String | Action that triggered the message<br>Valid value is "`Approved`" |
| `petitionSubmittedOn` | DateTime | Date the petition was submitted on |
| `plea` | String | Indicates what plea the Defendant took<br>Valid values are:<ul><li>"`Guilty`"</li><li>"`No Contest`"</li></ul> |
| `allRightsWaived` | Boolean | Indicates whether the Defendant waived all the rights |
| `adjudicated` | Boolean | Indicates whether the ase was adjudicated or not |
| `judgment` | String | Judgment<br>Valid value is "`Approved`" |
| `judgmentBy` | String | Judgment By |
| `judgmentByRole` | String | Judgment By Role<br>Valid values are:<ul><li>"`Court Clerk`"</li><li>"`Judicial Officer`"</li></ul> |
| `judgmentDate` | DateTime | Date the judgment was made on |
| `reducedAmountAccept` | Boolean | Indicates whether Reduction of Fines is accepted |
| `reducedAmountDeny` | Boolean | Indicates whether Reduction of Fines is denied |
| `totalDueAmount` | Number | Total Amount Due by the Defendant  |
| `finalReducedAmount` | Number | Final Reduced Amount |
| `recommendedReducedAmountPayByDate` | DateTime | ATP Tool recommended Date by when the Defendant has<br>to pay the reduced amount |
| `reducedAmountPayByDate` | DateTime | Date by when the Defendant has to pay the reduced amount |
| `paymentPlanAccept` | Boolean | Indicates whether Payment Plan is accepted |
| `paymentPlanDeny` | Boolean | Indicates whether Payment Plan is denied |
| `finalMonthlyPaymentAmount` | Number | Final Monthly Payment Amount |
| `monthlyPaymentDate` | DateTime | Monthly Payment Date |
| `moreTimeAccept` | Boolean | Indicates whether More Time to Pay is accepted |
| `moreTimeDeny` | Boolean | Indicates whether More Time to Pay is denied |
| `moreTimeToPayAmount` | Number | More Time to Pay Amount |
| `moreTimeToPayByDate` | DateTime | More Time to Pay by Date |
| `communityServiceAccept` | Boolean | Indicates whether Community Service is accepted |
| `communityServiceDeny` | Boolean | Indicates whether Community Service is denied |
| `judgeOrderedCommunityServiceHours` | Number | Number of Community Service Hours Judge ordered |
| `judgeOrderedCommunityServiceDueDate` | DateTime | Due Date by which the defendant has<br>to complete the Community Service Hours |

#### Sample Message:
```json
{
    "requestId" : "614e7eff8d5502c2f0d6336e",
    "caseNumber": "INF-19-C19037936",
    "citationNumber": "C19037936",
    "county": "San Francisco",
    "triggerAction": "Approved",
    "petitionSubmittedOn": "2019-01-01T03:59:15.561Z",
    "plea": "Guilty",
    "allRightsWaived": true,
    "adjudicated": false,
    "judgment": "Approved",
    "judgmentBy": "John Doe",
    "judgmentByRole": "County Clerk",
    "judgmentDate": "2019-01-01T03:59:15.561Z",
    "reducedAmountAccept": true,
    "reducedAmountDeny": null,
    "totalDueAmount": 1345,
    "finalReducedAmount": 269.00,
    "recommendedReducedAmountPayByDate": "2019-05-31T03:59:15.561Z",
    "reducedAmountPayByDate": "2019-05-31T03:59:15.561Z",
    "paymentPlanAccept": null,
    "paymentPlanDeny": null,
    "finalMonthlyPaymentAmount": null,
    "monthlyPaymentDate": null,
    "moreTimeAccept": null,
    "moreTimeDeny": null,
    "moreTimeToPayAmount": null,
    "moreTimeToPayByDate": null,
    "communityServiceAccept": null,
    "communityServiceDeny": null,
    "judgeOrderedCommunityServiceHours": null,
    "judgeOrderedCommunityServiceDueDate": null
}
```

### Petition is denied by Judicial Officer

| Name | Type | Description |
| :--- | :---: | :--- |
| `requestId` | String | Unique identifier for the request |
| `caseNumber` | String | Case Number |
| `citationNumber` | String | Citation Number |
| `county` | String | County |
| `triggerAction` | String | Action that triggered the message<br>Valid value is "`Rejected`" |
| `petitionSubmittedOn` | DateTime | Date the petition was submitted on |
| `judgment` | String | Judgment<br>Valid value is "`Rejected`" |
| `judgmentBy` | String | Judgment By |
| `judgmentByRole` | String | Judgment By Role<br>Valid values are:<ul><li>"`Court Clerk`"</li><li>"`Judicial Officer`"</li></ul> |
| `judgmentDate` | DateTime | Date the judgment was made on |
| `denialReason` | String | Reason for denying the petition |
| `rejectionDate` | DateTime | Date the petition was rejected on |

#### Sample Message:
```json
{
    "requestId" : "614e7eff8d5502c2f0d6336e",
    "caseNumber": "INF-19-C19037936",
    "citationNumber": "C19037936",
    "county": "San Francisco",
    "triggerAction": "Rejected",
    "petitionSubmittedOn": "2019-01-01T03:59:15.561Z",
    "judgment": "Rejected",
    "judgmentBy": "John Doe",
    "judgmentByRole": "County Clerk",
    "judgmentDate": "2019-01-01T03:59:15.561Z",
    "denialReason": "Petition is denied by Judicial Officer",
    "rejectionDate": "2019-01-01T03:59:15.561Z"
}
```

### Order is served by the Court Clerk

| Name | Type | Description |
| :--- | :---: | :--- |
| `caseNumber` | String | Case Number |
| `citationNumber` | String | Citation Number |
| `county` | String | County |
| `triggerAction` | String | Action that triggered the message<br>Valid value is "`Order Served`" |
| `petitionSubmittedOn` | DateTime | Date the petition was submitted on |
| `orderServedBy` | String | Order Served By |
| `orderServedStatus` | Boolean | Indicates whether the Order was served or not |
| `orderServedDate` | DateTime | Date the Order was served |
| `atpOrder` | String | Base64 format of the final ATP Order |

#### Sample Message:
```json
{
    "requestId" : "614e7eff8d5502c2f0d6336e",
    "caseNumber": "INF-19-C19037936",
    "citationNumber": "C19037936",
    "county": "San Francisco",
    "triggerAction": "Order Served",
    "petitionSubmittedOn": "2019-01-01T03:59:15.561Z",
    "orderServedBy": "John Doe",
    "orderServedStatus": true,
    "orderServedDate": "2019-01-01T03:59:15.561Z",
    "atpOrder": "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX"
}
```

### Order is vacated by the Court Administrator

| Name | Type | Description |
| :--- | :---: | :--- |
| `requestId` | String | Unique identifier for the request |
| `caseNumber` | String | Case Number |
| `citationNumber` | String | Citation Number |
| `county` | String | County |
| `triggerAction` | String | Action that triggered the message<br>Valid value is "`Vacated`" |
| `petitioner` | String | Petitioner |
| `petitionSubmittedOn` | DateTime | Date the petition was submitted on |
| `vacatedBy` | String | Vacated By |
| `vacatedDate` | DateTime | Date the Order was vacated |
| `vacatedReason` | String | Reason for vacating the order |

#### Sample Message:
```json
{
    "requestId" : "614e7eff8d5502c2f0d6336e",
    "caseNumber": "INF-19-C19037936",
    "citationNumber": "C19037936",
    "county": "San Francisco",
    "triggerAction": "Vacated",
    "petitioner": "John Doe",
    "petitionSubmittedOn": "2019-01-01T03:59:15.561Z",
    "vacatedBy": "John Doe",
    "vacatedDate": "2019-01-01T03:59:15.561Z",
    "vacatedReason": "Correction in case needed"
}
```
