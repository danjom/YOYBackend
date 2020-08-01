namespace YOY.Values
{
    public static class ContentSetTypes
    {
        public const int None = 0;
        public const int Slider = 1;
        public const int Carrousel = 2;
        public const int Grid = 3;
        public const int List = 4;
    }

    public static class OnSelectCellActionTypes
    {
        public const int None = 0;
        public const int DisplayPromotionContent = 1;
        public const int UpdateContentFilterOptions = 2;
        public const int UpdateDisplayedContent = 3;
        public const int AccessDealDetailScreen = 4;
        public const int AccessCashIncentiveDetailScreen = 5;
    }

    public static class ViewAllCellContentAccess
    {
        public const int None = 0;
        public const int CategoryList = 1;
        public const int CommerceList = 2;
        public const int ShoppingMallList = 3;
        public const int DealContentList = 4;
        public const int CashIncentiveContentList = 5;
    }

    public static class CellTypes
    {
        public const int None = 0;
        public const int Filter = 1;
        public const int Category = 2;
        public const int Commerce = 3;
        public const int ShoppingMall = 4;
        public const int Offer = 5;
        public const int CashIncentive = 6;
    }
    public static class FilterTypes
    {
        public const int None = 0;
        public const int Category = 1;
        public const int Commerce = 2;
        public const int ShoppingMall = 3;
    }

    public static class UserappErrorCustomActions
    {
        public const int None = 0;
        public const int PhoneValidationRequired = 1;
    }

    public static class DbActionTypes
    {
        public const int Add = 1;
        public const int Update = 2;
        public const int Delete = 3;
    }

    public static class ExceptionLayers
    {
        public const int DAO = 1;
        public const int Api = 2;
        public const int WebApp = 3;
    }

    public static class ApiKeyRequestedTypes
    {
        public const int None = 0;
        public const int Tenant = 1;
        public const int Branch = 2;
        public const int iotDevice = 3;
    }

    public static class TokenCredentialsType
    {
        public const int Password = 1;
        public const int RefreshToken = 2;
    }

    public static class OperationFlowOwners
    {
        public const int All = 0;
        public const int Employee = 1;
        public const int User = 2;
    }

    public static class OperationFlowTypes
    {
        public const int All = -1;
        public const int NotDetermined = 0;
        public const int GeneratePaymentRequest = 1;
        public const int DispatchPurchase = 2;
    }

    public static class OperationFlowSourceTypes
    {
        public const int All = 0;
        public const int Messaging = 1;
        public const int iOTDevice = 2;
    }

    public static class OperationFlowReferenceTypes
    {
        public const int All = -1;
        public const int None = 0;
        public const int MessageLog = 1;
        public const int PaymentRequest = 2;
        public const int Purchase = 3;
    }

    public static class PaymentRequestsSourceTypes
    {
        public const int All = 0;
        public const int Messaging = 1;
        public const int iOTDevice = 2;
        public const int Purchase = 3;
    }

    public static class PurchaseDispatchValidatorSourceTypes
    {
        public const int Undefined = -1;
        public const int All = 0;
        public const int Messaging = 1;
        public const int iOTDevice = 2;
    }

    public static class BusinessPortalAccessLevels
    {
        public const int NoAccess = 0;
        public const int ViewOnly = 1;
        public const int ActiveStatusUpdater = 2;
        public const int CreatorUpdater = 3;
        public const int FullAccess = 4;
    }
    public static class UserPreferenceResponseContentType
    {
        public const int None = 0;
        public const int FullPreferenceSet = 1;
        public const int OnlyCategories = 2;
    }

    public static class RoleNames
    {
        public const string Master = "Master";
        public const string SuperAdmin = "SuperAdmin";
        public const string Admin = "Admin";
        public const string Operator = "Operator";
    }

    public static class PaginationDefaults
    {
        public const int PageSize = 150;
        public const int PageNumber = 0;
    }

    public static class TextMessageLogReferenceTypes
    {
        public const int All = 0;
        public const int None = 1;
        public const int Offer = 2;
        public const int CashbackIncentive = 3;
        public const int Image = 4;
        public const int Video = 5;
    }

    public static class TextMessageLogPurposeTypes
    {
        public const int All = 0;
        public const int AccountValidation = 1;
        public const int Marketing = 2;
        public const int Discover = 3;
        public const int OperationAction = 4;
        public const int GeneratePaymentRequest = 5;
        public const int DispatchPurchase = 6;
    }

    public static class TextMessageLogStatuses
    {
        public const int FailedDelivery = -1;
        public const int All = 0;
        public const int Unknown = 1;
        public const int SentRequested = 2;
        public const int SuccessfullyDelivered = 3;
        public const int ReadByUser = 4;
    }

    public static class TextMessageGateways
    {
        public const int All = 0;
        public const int Twilio = 1;
    }
    public static class TextMessageChannels
    {
        public const int All = 0;
        public const int SMS = 1;
        public const int Whatsapp = 2;
    }

    public static class FeaturedSlideTypes
    {
        public const int All = 0;
        public const int PlatformPropietary = 1;
        public const int Sponsored = 2;
        public const int SpecificCampaign = 3;
    }

    public static class HardwareIOTDeviceTypes
    {
        public const int All = 0;
        public const int PaymentDevice = 1;
    }

    public static class HardwareIOTDateTypes
    {
        public const int All = 0;
        public const int InstallationDate = 1;
        public const int LastRequestDate = 2;
        public const int LastMaintenanceDate = 3;
    }

    public static class HardwareIOTDeviceStatuses
    {
        public const int All = 0;
        public const int Unassigned = 1;
        public const int NotConfigured = 2;
        public const int InstalledOnTenant = 3;
        public const int ActivelyWorking = 4;
        public const int IssuesReported = 5;
        public const int OnMaintenance = 6;
        public const int Damaged = 7;
    }

    public static class TargettingParamMarks
    {
        public const string Gender = "G";
        public const string AgeInterval = "A";
        public const string AnyValue = "#";
        public const string ParamsSeparator = "*";
        public const string ValueSeparator = "-";
        public const string TypeValueSeparator = ":";
    }

    public static class EarningsIncreaserAccessTypes
    {
        public const int All = 0;
        public const int OnPayment = 1;
        public const int OnPurchase = 2;
    }

    public static class EarningsIncreaserTypes
    {
        public const int All = 0;
        public const int Base = 1;
        public const int Special = 2;
        public const int Promotional = 3;
        public const int Premium = 4;
    }

    public static class EarningsIncreaserUnlockerTypes
    {
        public const int All = 0;
        public const int OpenAccess = 1;
        public const int RewardRedemption = 2;
    }

    public static class EarningsIncreaserFactorTypes
    {
        public const int All = 0;
        public const int Percentage = 1;
        public const int FixedAmount = 2;
    }

    public static class CurrencyTypes
    {
        public const int CostaRicanColon = 1;
        public const int USDollar = 2;
        public const int GuatemalanQuetzal = 3;
        public const int HonduranLempira = 4;
        public const int NicaraguanCordoba = 5;
        public const int MexicanPeso = 6;
        public const int ColombianPeso = 7;
    }

    public static class CurrencySymbols
    {
        public const string CostaRicanColon = "₡";
        public const string USDollar = "US$";
        public const string GuatemalanQuetzal = "Q";
        public const string HonduranLempira = "L";
        public const string NicaraguanCordoba = "C$";
        public const string MexicanPeso = "MX$";
        public const string ColombianPeso = "COL$";
    }

    public static class FundingTypes
    {
        public const int All = 0;
        public const int Debit = 1;
        public const int Credit = 2;
    }

    public static class PaymentInfoStatuses
    {
        public const int All = 0;
        public const int Added = 1;
        public const int ValidationPending = 2;
        public const int PayEnabled = 3;
        public const int OnHold = 4;
        public const int PayDisabled = 5;
        public const int Blocked = 6;
    }

    public static class MoneyConversions
    {
        public const decimal MexicanValue = 10;
        public const decimal CostaRicanValue = 50;
        public const decimal ColombianValue = 100;
        public const decimal USValue = 1;
    }

    public static class GenericConfigValues
    {
        public const string SupportEmail = "soporte@clubyoy.com";
        public const string CostaRicaSupportPhoneNumber = "+506 4001-5147";
        public const string SupportChatLink = "https://clubyoy.com";
        public const int FreeTrialMonthsPeriod = 3;
        public const int MinCommissionAmount = 7;
        public const decimal AdditionalPercentageFeeForNoSpecialPrice = 10;
        public const double FrequentClaimPenalizer = 0.7;
        public const int FrequentClaimAcceptance = 3;
        public const int FrequentClaimMinsLaps = 60;
        public const char UniqueCodeMark = '$';
        public const int ClaimRefCodeLength = 5;
        public const int MaxMinutesToShareClaimRefCode = 45;
    }

    public static class LoyaltyProgramTypes
    {
        public const int None = 0;
        public const int Basic = 1;
        public const int Plus = 2;
        public const int Premium = 3;
    }

    public static class TextProcessingResultCode
    {
        public const int None = 0;
        public const int Validated = 1;
        public const int UnableToValidate = 2;
        public const int Rejected = 3;
    }

    public static class ReceiptCurrencySymbolUsageTypes
    {
        public const int None = 0;
        public const int OnlyForTotalAmount = 1;
        public const int OnlyForPurchasedItems = 2;
        public const int PurchasedItemsAndAmount = 3;
    }

    public static class ReceiptStructureTypes
    {
        public const int BusinessInfoFirts = 1;
        public const int PurchasedItemsFirst = 2;
    }

    public static class ReceiptClaimMarkTypes
    {
        public const int None = 0;
        public const int InPurchasedItem = 1;
        public const int InTicketInfoAppName = 2;
        public const int InTicketInfoAccNumber = 3;
        public const int InTicketInfoRefCode = 4;
    }

    public static class TenantRelevanceStatuses
    {
        public const int All = -1;
        public const int None = 0;
        public const int SingleLocation = 1;
        public const int SmallBusiness = 2;
        public const int CityBusiness = 3;
        public const int StateBusiness = 4;
        public const int RegionalBusiness = 6;
        public const int NationalBusiness = 7;
        public const int InternationalBusiness = 8;
        public const int AnchorBusiness = 9;
    }

    public static class CategoryPreferenceRelevanceStatuses
    {
        public const int All = -1;
        public const int Miscellaneous = 1;
        public const int ThirdNeed = 2;
        public const int SecondNeed = 3;
        public const int FirstNeed = 4;
    }

    public static class CategoryRelationTypes
    {
        public const int All = 0;
        public const int Tenant = 1;
        public const int Offer = 2;
    }

    public static class CashierAppClaimTypes
    {
        public const int CodeScanning = 1;
        public const int Listing = 2;
    }

    public static class MoneraryDistribution
    {
        public const decimal Platform = 0.7M;
        public const decimal User = 0.3M;
    }

    public static class MembershipLevels
    {
        public const int Bronze = 1;
        public const int Silver = 2;
        public const int Gold = 3;
        public const int Platinum = 4;
        public const int Diamond = 5;
    }

    public static class UserEarningPerLevel
    {
        public const decimal Bronze = 0.75M;
        public const decimal Silver = 0.9M;
        public const decimal Gold = 1.1M;
        public const decimal Platinum = 1.3M;
        public const decimal Diamond = 1.5M;
    }

    public static class MoneyBalaceRefTypes
    {
        public const int Category = 1;
        public const int Deal = 2;
    }

    public static class WithdrawalStatuses
    {
        public const int Failed = -1;
        public const int All = 0;
        public const int Requested = 1;
        public const int Approved = 2;
        public const int SuccessfullyProccessed = 3;
        public const int Rejected = 4;
    }

    public static class WithdrawalServiceTypes
    {
        public const int All = -1;
        public const int BankTransfer = 1;//Banks
        public const int PaymentGateway = 2;//Paypal, Stripe, ...
        public const int MoneyExpiditTransfer = 3;//Moneygram, Western Union, ...
    }

    public static class PaymentGateways
    {
        public const int All = -1;
        public const int Cardinal = 1;
        public const int SINPE = 2;
        public const int Stripe = 3;//INTERNATIONAL
    }

    public static class TransferTypes
    {
        public const int All = -1;
        public const int WireTransfer = 1;//Banks
        public const int SINPE = 2;//ONLY FOR COSTA RICA
        public const int PayPal = 3;//INTERNATIONAL
    }

    public static class MoneyWithdrawalServiceRefType
    {
        public const int All = -1;
        public const int AccountNumber = 1;//Banks
        public const int CardNumber = 2;//Paypal, Stripe, ...
        public const int ServiceExternalId = 3;//Moneygram, Western Union, ...
    }

    public static class MoneyWithdrawalStatuses
    {
        public const int All = -1;
        public const int Requested = 1;
        public const int Approved = 2;
        public const int Completed = 3;
        public const int Rejected = 4;
    }

    public static class MoneyWithdrawServiceAccountRefTypes
    {
        public const int All = -1;
        public const int BankAccId = 1;
        public const int LinkedMobilePhone = 2;
        public const int UserId = 3;
    }

    public static class MonetaryFeeLogTypes
    {
        public const int All = -1;
        public const int AccountPayable = 1;
        public const int AccountReceivable = 2;
    }

    public static class MonetaryFeeLogsTenantTypes
    {
        public const int All = -1;
        public const int Debtor = 1;
        public const int Collector = 2;
    }

    public static class MonetaryFeeLogRefTypes
    {
        public const int All = -1;
        public const int None = 0;
        public const int UsageRecordLine = 1;
        public const int MoneyConversionLog = 2;
        public const int SurveyResponse = 3;
        public const int WithdrawalRequest = 4;
        public const int MonetaryFeeLog = 5;
    }

    public static class MonetaryFeeLogReasons
    {
        public const int All = -1;
        public const int DealClaim = 1;
        public const int ReceiptScan = 2;
        public const int PaymentWithPoints = 3;
        public const int SurveyResponded = 4;
        public const int AdDisplay = 5;
        public const int LoyaltyProgramFee = 6;
        public const int MoneyWithdrawal = 7;
        public const int Taxes = 8;
    }

    public static class MonetaryFeeLogStatuses
    {
        public const int All = -1;
        public const int Payed = 1;
        public const int PaymentPending = 2;
        public const int Disputed = 3;
        public const int Uncollectible = 4;
    }

    public static class MonetaryCreditCodeAssignedStates
    {
        public const int All = -1;
        public const int NotAssigned = 1;
        public const int Assigned = 2;
    }

    public static class MonetaryCreditCodeSyncedStates
    {
        public const int All = -1;
        public const int NotSynced = 1;
        public const int Synced = 2;
    }

    public static class MonetaryCreditCodeRefTypes
    {
        public const int All = -1;
        public const int PaymentVoucher = 1;
        public const int Giftcard = 2;
    }

    public static class MonetaryCreditCodeChangeStateTypes
    {
        public const int Assigned = 1;
        public const int Synced = 2;
    }

    public static class MoneyConversionLogInternalStatuses
    {
        public const int Created = 1;
        public const int ValidationPending = 2;
        public const int PaymentPending = 3;
        public const int Payed = 4;
        public const int Alert = 5;
    }

    public static class MoneyConversionLogStates
    {
        public const int FailedRegistration = -1;
        public const int PointsConverted = 0;
        public const int Registered = 1;
        public const int AlreadyUsed = 2;
        public const int Expired = 3;
        public const int CommerceMismatch = 4;
        public const int UserMismatch = 5;
        public const int Invalid = 6;
    };

    public static class SectionIcons
    {
        public const string FeaturedDeals = "https://res.cloudinary.com/yoyimgs/image/upload/v1524921173/yoy-preferences/img-populares.png";
        public const string UserTargettedDeals = "https://res.cloudinary.com/yoyimgs/image/upload/v1524921173/yoy-preferences/img-para-mi.png";
        public const string NearbyDeals = "https://res.cloudinary.com/yoyimgs/image/upload/v1524921173/yoy-preferences/img-cercanos.png";
    }

    public static class EarnPointsAlertIcons
    {
        public const int None = 0;
        public const int Money = 1;
        public const int Gift = 2;
    }

    public static class MeasureUnits
    {
        public const string Kilometers = "Km";
        public const string Miles = "mi";
    }

    public static class FileDownloadTypes
    {
        public const int Complete = 1;
        public const int Chuncks = 2;
    }

    public static class BroadcasterParamValues
    {
        public const int RecentDetectionMins = -10;
        public const int BroadcasterSymbolDecimalLength = 3;
        public const int BroadcasterSymbolBinLength = 10;
        public const char TriggerSymbolsSeparator = '.';
        public const int SimpleTriggerSymbolsCount = 1;
        public const int CompoundTriggerSymbolsCount = 3;
    }

    public static class BroadcasterUsageTypes
    {
        public const int All = -1;
        public const int InPlace = 1;
        public const int OnTheFence = 2;
        public const int MediaInteraction = 3;
    }

    public static class BroadcasterPurposeTypes
    {
        public const int All = -1;
        public const int BroadcastingExclusive = 1;
        public const int WalkInCheckInExclusive = 2;
        public const int BroadcastingAndWalkInCheckIn = 3;
    }

    public static class BroadcastingLogRecordChangeTypes
    {
        public const int All = -1;
        public const int ViewCount = 1;
        public const int RedemptionCount = 2;
    }

    public static class SearchableObjectTypes
    {
        public const int All = -1;
        public const int None = 0;
        public const int Deal = 1;
        public const int CashbackIncentive = 2;
        public const int Category = 3;
        public const int Commerce = 4;
        public const int ShoppingMall = 5;
    }

    public static class SearchIndexes
    {
        public const string dev_DEALS = "113a4d94-5300-4dec-a555-14890a364f1e";
        public const string dev_CLUBS = "093a4d94-53db-4dec-a555-14890b364fba";
        public const int dealsIndex = 1;
        public const int clubIndex = 2;
    }

    public static class SearchIndexNames
    {
        public const string AppName = "HDFTAAQXVP";
        public const string ProdAppend = "prod_";
        public const string DevAppend = "dev_";
        public const string GeneralContent = "GENERALCONTENT";
        public const string CommercePreferences = "COMMERCEPREFERENCES";
        public const string CashbackIncentives = "CASHBACKINCENTIVES";
    }

    public static class SearchSources
    {
        public const int App = 1;
        public const int Website = 2;
    }

    public static class SearchLimits
    {
        public const int MaxTrendingSearches = 10;
        public const int DaysForTrendingSearches = 15;
    }

    public static class DataLocationReferences
    {
        public const int None = 0;
        public const int GPSLocation = 1;
        public const int SelectedLocation = 2;
    }

    public static class DataSectionDisplayModes
    {
        public const int Carrousel_TableView = 1;
        public const int CarrouselOnly = 2;
        public const int TableViewOnly = 3;
    }

    public static class Languages
    {
        public const int Spanish = 1;
        public const int English = 2;
    }

    public static class HttpcallTypes
    {
        public const int Post = 1;
        public const int Put = 2;
        public const int Get = 3;
        public const int Delete = 4;
    }

    public static class SourceAPIs
    {
        public const int MobileApp = 1;
        public const int Business = 2;
        public const int Web = 3;
    }

    public static class OperatingSystems
    {
        public const int Unknown = 0;
        public const int Android = 1;
        public const int iOS = 2;
    }

    public static class StatusCodes
    {
        public const int Ok = 200;
        public const int Accepted = 202;
        public const int Ambiguous = 300;
        public const int BadRequest = 400;
        public const int Unauthorized = 401;
        public const int Forbidden = 403;
        public const int NotFound = 404;
        public const int NoContent = 204;
        public const int Conflict = 409;
        public const int InternalServerError = 500;
    }

    public static class ApiControllers
    {
        public const int Deal = 1;
        public const int BroadcastingHistory = 2;
        public const int Transaction = 3;
        public const int UserPreference = 4;
        public const int Account = 5;
        public const int Location = 6;
        public const int Geolocation = 7;
        public const int Club = 8;
        public const int Membership = 9;
        public const int UserConfirmationCode = 10;
        public const int MembershipOperation = 11;
        public const int PlayerLog = 11;
        public const int IssueReporting = 12;
        public const int TicketReader = 13;
        public const int MoneyWithdraw = 14;
    }

    public static class TenantStructureTypes
    {
        public const int Complete = 1;
        public const int Min = 2;
    }

    public static class Currencies
    {
        public const string Dollar = "US$";
        public const string Colon = "¢";
        public const string Pesos = "$";
    }

    public static class DealTypes
    {
        public const int None = -1;
        public const int All = 0;
        public const int InStore = 1;
        public const int Online = 2;
        public const int Phone = 3;
        public const int Catalog = 4;
        public const int Info = 5; //Single Image Static or Animated
        public const int Video = 6;
        public const int Survery = 7;//Survey
        public const int Event = 8;//Event
    }

    public static class DeliveryTypes
    {
        public const int All = 0;
        public const int ClaimInStore = 1;
        public const int ClaimPickup = 2;
        public const int ClaimOnline = 3;
        public const int ClaimByPhone = 4;
    }

    public static class BTLContentTypes
    {
        public const int All = 0;
        public const int Catalog = 1;
        public const int SingleImage = 2;
        public const int AnimatedImage = 3;
        public const int Video = 4;
        public const int Link = 5;
    }

    public static class ProductItemContentTypes
    {
        public const int All = 0;
        public const int SingleImage = 1;
        public const int AnimatedImage = 2;
        public const int Video = 3;
        public const int Link = 4;
        public const int Survey = 5;
    }

    public static class ProductItemContentTriggerActions
    {
        public const int All = 0;
        public const int Scanning = 1;
        public const int Purchasing = 2;
    }

    public static class UserProductItemInteractionTypes
    {
        public const int All = 0;
        public const int Scanning = 1;
        public const int Purchasing = 2;
    }

    public static class UserProductItemInteractionStates
    {
        public const int All = 0;
        public const int ValidationPending = 1;
        public const int AutomaticallyValidated = 2;
        public const int ManuallyValidationRequired = 3;
        public const int Rejected = 4;
    }
    public static class AmountsMatchStatuses
    {
        public const int All = -1;
        public const int NotDetermined = 0;
        public const int TotalAmountGreater = 1;
        public const int ItemsPricesGreater = 2;
        public const int Match = 3;
    }

    public static class ReceiptPurposes
    {
        public const int All = -1;
        public const int None = 0;
        public const int ValidateDealClaim = 1;
        public const int ValidateProductPurchase = 2;
    }

    public static class ReceiptRequestedValidationReferenceTypes
    {
        public const int All = -1;
        public const int Deal = 1;
        public const int Loyalty = 2;
    }

    public static class ReceiptValidationStatuses
    {
        public const int All = -1;
        public const int None = 0;
        public const int Submitted = 1;
        public const int AutomaticallyValidated = 2;
        public const int RequiresManualValidation = 3;
        public const int ManuallyValidated = 4;
        public const int Invalid = 5;
        public const int RequiresPurchasedAmountValidation = 6;
        public const int RequiresResubmission = 7;
        public const int Rejected = 8;
    }

    public static class ReceiptExtrationStatuses
    {
        public const int Failed = -1;
        public const int All = 0;
        public const int None = 1;
        public const int AutomaticallyExtracted = 2;
        public const int RequiresManualExtraction = 3;
        public const int ManuallyExtracted = 4;
    }

    public static class RewardTypes
    {
        public const int Deal = 1;//This is the most general one and the mostly used
        public const int Prize = 2;
        public const int Game = 3;
        public const int Gift = 4;
    }


    public static class SavingRouteChangeTypes
    {
        public const int ActiveState = 0;
        public const int OrderedState = 1;
    }

    public static class SavingRouteParticipantStatuses
    {
        public const int All = -1;
        public const int ActivePlayer = 0;
        public const int RouteAccomplished = 1;
        public const int RouteAccomplisherVerified = 2;
        public const int RewardsUnlocked = 3;
        public const int IncompleteRoute = 4;
        public const int CheatingSuspect = 5;
        public const int Banned = 6;
    }

    public static class SavingRouteParticipantRecordValidationTypes
    {
        public const int All = -1;
        public const int Software = 0;
        public const int Manual = 1;
    }

    public static class SavingRouteParticipantRecordStatuses
    {
        public const int All = -1;
        public const int Registered = 0;
        public const int ReceiptPhotoSent = 1;
        public const int OnVerification = 2;
        public const int Verified = 3;
        public const int Rejected = 4;
    }

    public static class SavingRouteParticipantRecordChangeTypes
    {
        public const int Status = 0;
        public const int ValidationType = 1;
    }

    public static class SavingRouteMemberStatuses
    {
        public const int All = -1;
        public const int Invited = 0;
        public const int InvitationAccepted = 1;
        public const int OfferSet = 2;
        public const int OfferApproved = 3;
        public const int OfferRejected = 4;
        public const int Expired = 5;
    }

    public static class SavingRouteMemberReferenceTypes
    {
        public const int Route = 0;
        public const int Tenant = 1;
    }

    public static class SavingRouteMemberChangeTypes
    {
        public const int Status = 0;
        public const int Participants = 1;
    }

    public static class RewardToAwardStatuses
    {
        public const int All = -1;
        public const int Awarded = 0;
        public const int ShownToUser = 1;
        public const int Redeemed = 2;
    }

    public static class RewardToAwardOrginatorTypes
    {
        public const int All = -1;
        public const int SavingRoute = 0;
    }

    public static class RewardStates
    {
        public const int All = -1;
        public const int OpenRedemption = 1;
        public const int RaffleParticipating = 2;
        public const int RaffleWinner = 3;
        public const int Locked = 4;
    }

    public static class SavingRouteParticipantRecordReferenceTypes
    {
        public const int All = -1;
        public const int Route = 0;
        public const int Member = 1;
        public const int Participant = 2;
        public const int TenantId = 3;
    }

    public static class CellActionTypes
    {
        public const int ShowContentDetail = 1;
        public const int DisplayDealsListView = 2;
        public const int OpenWebView = 3;
    }

    public static class ContentFilters
    {
        public const int NoFilter = 0;
        public const int Tenant = 1;
        public const int Category = 2;
        public const int Recommended = 3;
    }

    public static class ImageDimensions
    {
        public const int OfferWidth = 640;
        public const int OfferHeight = 514;
        public const int LogoWidth = 245;
        public const int LogoHeight = 245;
        public const int LandingWidth = 656;
        public const int LandingHeight = 362;
    }

    public static class ImageRequesters
    {
        public const int App = 1;
        public const int Website = 2;
    }

    public static class ImageStorages
    {
        public const int Cloudinary = 2;
    }

    public static class ImageFolders
    {
        public const string ProdFolder = "prod/";
        public const string DevFolder = "dev/";
        public const string Deals = "deals";
        public const string Logos = "logos";
        public const string BTL = "btl";
        public const string Slides = "slide";
        public const string EmailAssets = "email_assets";
        public const string ProfilePics = "profile_pics";
    }

    public static class TimerTypeMinuteRanges
    {
        public const int Date = 21600;//More than 2 WEEKS
        public const int CountDown = 60;//More than 1 HOUR
        public const int FastCountDown = 0;//More than 0 MINS
    }

    public static class InterestScoreActioners
    {
        public const int Geofence = 1;
        public const int Broadcaster = 2;
        public const int Redemption = 3;
        public const int Claim = 4;
        public const int VisitRegistry = 5;
    }

    public static class CampaignPenaltyPercentages
    {
        public const int MissedRedemptions = 7;
        public const int MissedClaims = 4;
    }
    public static class InterestScoreBasePoints
    {
        public const int TenantInterestCreation = 50;
        public const int ProductCatgoryInterestCreation = 80;
        public const int Geofence = 10;
        public const int Broadcaster = 20;
        public const int Redemption = 100;
        public const int Claim = 200;
        public const int CommerceGeofence = 5;
        public const int CommerceBroadcaster = 10;
        public const int CommerceRedemption = 50;
        public const int CommerceClaim = 100;
    }

    public static class InterestOrigintTypes
    {
        public const int UserSelection = 1;
        public const int UserActions = 2;
        public const int MachineLearning = 3;
    }

    public static class BroadcastingLimits
    {
        public const int MaxPerDay = 9;
        public const int MaxPerDayPerTenant = 3;
        public const int MaxOffersPerBroadcasting = 5;
        public const int MaxIneffectiveSentsPerCampaign = 6;
        public const decimal MaxDifferenceScorePercentage = 40;
        public const int SameGeofenceTimeOutSecs = 1800;
        public const int SameBroadcasterTimeOutSecs = 60;
        public const int GeofenceToGeofenceTimeOutSecs = 300;
        public const int GeofenceToBroadcasterTimeOutSecs = 180;
        public const int BroadcasterToGeofenceTimeOutSecs = 240;
        public const int BroadcasterToBroadcasterTimeOutSecs = 30;
        public const int PushToOtherTimeOutSecs = 0;
    }

    public static class BroadcastingTimeOutLimitMinutes
    {
        public const int Geofence_Geofence = 30;
        public const int Beacon_Beacon = 2;
        public const int Geofence_Beacon = 5;
        public const int Beacon_Geofence = 5;
        public const int SameCampaign = 2160;
        public const int CampaignOfferRedemption = 2160;
        public const int CampaignOfferClaim = 1440;
    }

    public static class ChronologicalOrders
    {
        public const int Descending = 1;
        public const int Ascending = 2;
    }

    public static class ContentPurposeTypes
    {
        public const int Display = 1;
        public const int Broadcasting = 2;
    }

    public static class AppSectionReferenceTypes
    {
        public const int Transactions = 1;
        public const int Offer = 2;
        public const int Catalog = 3;
    }

    public static class TransactionLocationActionTypes
    {
        public const int Redemption = 1;
        public const int Claim = 2;
    }
    public static class TransactionLocationReferenceTypes
    {
        public const int Transaction = 1;
        public const int Broadcaster = 2;
    }
    public static class CustomTransactionActions
    {
        public const int DismissModal = 0;
        public const int GoToRedemptionDetail = 1;
        public const int OpenURL = 2;
        public const int PhoneCall = 3;
        public const int PlayVideo = 4;
        public const int StartGame = 5;
        public const int DisplaySurvey = 6;
        public const int DisplayForm = 7;
    }

    public static class TriggerActivatorTypes
    {
        public const int Geofence = 1;
        public const int Broadcaster = 2;
    }

    public static class BroadcastingEventTypes
    {
        public const int Detection = 0;
        public const int Entering = 1;
        public const int Exiting = 2;
        public const int Dwelling = 3;
        public const int Remote = 4;//for push notifications
    }

    public static class GeofenceFilters
    {
        public const int Geofence = 1;
        public const int Geotrigger = 2;
    }

    public static class GeotriggerTypes
    {
        public const int Enter = 1;
        public const int Exit = 2;
        public const int Dwell = 3;
    }

    public static class LocationReferenceTypes
    {
        public const int Country = 0;
        public const int State = 1;
        public const int City = 2;
        public const int District = 3;
    }

    public static class RaffleTypes
    {
        public const int Open = 0;//Anyone with enough points can claim reward
        public const int ByRaffle = 1;//Just the selected users has the chance to claim reward
        public const int PerPurchases = 2;//This is unlocked after a certain amount of purchases
    }

    public static class RewardUsageTypes
    {
        public const int All = -1;
        public const int AllPurpose = 0;//Anyone with enough points can claim reward
        public const int YOYBenefits = 1;//Just the selected users has the chance to claim reward
        public const int CommerceLoyaltyProgram = 2;//This is unlocked after a certain amount of visits
    }

    public static class RaffleIdTypes
    {
        public const int Raffle = 0;
        public const int Reward = 1;
    }

    public static class TimerTypes
    {
        public const int NoTimer = 0;
        public const int Date = 1;
        public const int CountDown = 2;
        public const int FastCountDown = 3;
        public const int FastCountDownIncentiveVariation = 4;
    }

    public static class DisplayTypes
    {
        public const int All = -1;
        public const int ListingsOnly = 1;
        public const int BroadcastingAndListings = 2;
        public const int BroadcastingOnly = 3;
        public const int UnlockCodeRequired = 4;
    }

    public static class ScheduleTypes
    {
        public const int NoSchedule = 0;
        public const int Continously = 1;
        public const int Segmented = 2;
    }

    public static class OfferTypes
    {
        public const int Reward = 1;
        public const int Offer = 2;
        public const int Coupon = 3;
        public const int CashbackIncentive = 4;
        public const int Special = 5;
    }

    public static class ExtraBonusTypes
    {
        public const int All = -1;
        public const int None = 0;
        public const int Percentage = 1;
        public const int FixedAmount = 2;
    }

    public static class CashIncentiveBenefitAmountTypes
    {
        public const int All = -1;
        public const int ByTotalAmount = 1;
        public const int ByAmountBlock = 2;
    }

    public static class CashbackTypes
    {
        public const int All = -1;
        public const int None = 0;
        public const int Percentage = 1;
        public const int FixedAmount = 2;
        public const int Points = 3;
    }

    public static class CashbackApplyTypes
    {
        public const int All = -1;
        public const int WalletIncrease = 1;
        public const int DirectDiscount = 2;
    }

    public static class EarningsLimitTypes
    {
        public const int All = -1;
        public const int ByPercentage = 1;//Max cashback percentage
        public const int ByDirectAmount = 2;//Max cashback amount directly generated by the earnings increaser
        public const int ByTotalAmount = 3;//Max cashback amount compound by the earnings increaser + base cashback
    }

    public static class AffinityTypes
    {
        public const int Category = 1;
        public const int Tenant = 2;
    }

    public static class DealContentTypes
    {
        public const int All = -1;
        public const int None = 0;
        public const int Offer = 1;
        public const int Catalog = 2;
        public const int Link = 3;// URL
        //The ones below are for carrousels purposes, aren't deal types
        public const int Tenant = 4;
        public const int Category = 5;
        public const int MembershipBalance = 6;
        //this ones are also deal types
        public const int Event = 7;
        public const int SingleImage = 8;
        public const int AnimatedImage = 9;
        public const int Video = 10;
        public const int Survey = 11;
        public const int PerVisitsReward = 12;
    }
    public static class SavedItemReferenceTypes
    {
        public const int All = 0;
        public const int Offer = 1;
        public const int CashbackIncentive = 2;
        public const int ProductItem = 3;// For supermarkets
    }

    public static class IncentiveContentTypes
    {
        public const int All = -1;
        public const int None = 0;
        public const int Cashback = 1;
    }

    public static class ScoreAspects
    {
        public const int Price = 1;
        public const int Quality = 2;
        public const int CustomerService = 3;
        public const int Convenience = 4;
        public const int Ease = 5;
        public const int Location = 6;
    }

    public static class CountryCodes
    {
        public const string CostaRica = "CRC";
        public const string Mexico = "MX";
        public const string Colombia = "CO";
    }

    public static class ISRTaxPercentages
    {
        public const decimal CostaRica = 0.3M;
        public const decimal Mexico = 0.3M;
        public const decimal Colombia = 0.3M;
    }

    public static class SalesTaxPercentages
    {
        public const decimal CostaRica = 0.13M;
        public const decimal Mexico = 0.16M;
        public const decimal Colombia = 0.18M;
    }

    public static class GeoSegmentationTypes
    {
        public const int All = 0;
        public const int Country = 1;
        public const int State = 2;
        public const int City = 3;
    }

    public static class GeoSegmentationReferenceTypes
    {
        public const int Offer = 1;
        public const int BTLContent = 2;
        public const int CashbackIncentive = 3;
    }

    public static class CategoryRelatiomReferenceTypes
    {
        public const int Tenant = 1;
        public const int Offer = 2;
    }

    public static class ContentLocationRetrievedDataTypes
    {
        public const int All = 0;
        public const int Location = 1;
        public const int Content = 2;
    }

    public static class BroadcastingTriggerDataComponentsCounts
    {
        public const int Geofencing = 5;
        public const int Broadcaster = 5;
    }

    public static class BroadcastingChannelTypes
    {
        public const int All = 0;
        public const int PushNotification = 1;
        public const int Beacon = 2;
        public const int Geofence = 3;
        public const int Image = 4;
    }

    public static class ProductOperations
    {
        public const int Redemption = 1;
        public const int Claim = 2;
        public const int Increase = 3;
    }

    public static class BroadcastingDevicesReferenceTypes
    {
        public const int All = -1;
        public const int Branch = 1;
        public const int Department = 2;
        public const int Tenant = 3;
    }

    public static class CodeTypes
    {
        public const int Text = 1;
        public const int Img = 2;
        public const int UniqueCodes = 3;
    }

    public static class LoyaltyPointsEventTypes
    {
        public const int All = -1;
        public const int CheckIn = 1;
        public const int ClaimDeal = 2;
        public const int RedeemReward = 3;
        public const int ReceiveTransfer = 4;
        public const int PayWithPoints = 5;
    }

    public static class PointsEarnStatuses
    {
        public const int All = -1;
        public const int NotElegible = 0;
        public const int NoPointsToEarn = 1;
        public const int Elegible = 2;
        public const int NotElegibleDueUnavailableLocation = 3;
        public const int ElegibleByDealType = 4;
        public const int ElegibleByCoordinates = 5;
        public const int ElegibleBySignal = 6;
        public const int GrantedByReceiptSubmission = 7;
        public const int ReceiptSubmmittedRequiresValidation = 8;
        public const int DirectlyGranted = 9;
    }

    public static class VoucherCodeRefTypes
    {
        public const int All = -1;
        public const int Offer = 1;
        public const int Event = 2;
    }

    public static class VoucherCodeStatuses
    {
        public const int All = -1;
        public const int Available = 1;
        public const int Assigned = 2;
        public const int Used = 3;
    }

    public static class ViewCodeButtonActions
    {
        public const int NoAction = 0;
        public const int CopyToClipboard = 1;
    }

    public static class InterestTypes
    {
        public const int All = 0;
        public const int Category = 1;
        public const int Tenant = 2;
    }

    public static class GenderParams
    {
        public const char Any = '#';
        public const char MostLikelyMale = 'M';
        public const char OnlyMale = 'W';
        public const char MostLikelyFemale = 'F';
        public const char OnlyFemale = 'T';
        public const char NotSpecified = '-';
    }

    public static class ProfileGenders
    {
        public const char Male = 'M';
        public const char Female = 'F';
        public const char NotSpecified = '-';
    }

    public static class ParamValueTypes
    {
        public const int Interval = 1;
        public const int GivenValue = 2;
    }

    public static class ParamDataTypes
    {
        public const int Int = 1;
        public const int Date = 2;
        public const int String = 3;
        public const int Boolean = 4;
    }

    public static class MembershipActionTypes
    {
        public const int OfferClaimed = 1;
        public const int OfferRedeemed = 2;
        public const int RewardClaimed = 3;
    }

    public static class MembershipInterestDates
    {
        public const int LastOfferReserved = 1;
        public const int LastOfferClaimed = 2;
        public const int LastLevelEvaluation = 3;
    }

    public static class MembershipConfigValues
    {
        public const string BaseCommerceId = "00000000-0000-0000-0000-000000000000";
        public const float Equivalence = 0.1F;
        public const int WalkInCheckInLoyaltyDivider = 5;
        public const float RateDealFactor = 0.25F;
        public const int DefaultCheckInPoints = 0;
        public const int DefaultPointsLifeSpanMonths = 12;
        //public const float DefaultMultiplierFactor = 1F;
        //public const int DefaultMaxGeneratedPoints = 250;
        //public const int DefaultMinGeneratedPoints = 100;
        //public const int DefaultEvaluationMonths = 3;
        //public const double DefaultMonetaryConversionFactory = 0.1F;
        public const int SoonToExpireDaysLeftIndicator = 45;
        public const int AmountOfPointsToCalculatePointsMoneyEquivalence = 100;
        public const int MinsToShowConversionCode = 6;
        public const int MoneyWithdrawalEnabled = 1;
        public const int MoneyWithdrawalFollowUpCodeLength = 7;
        public const int MaxDaysToTransferMoney = 7;
        public const float WalletCashBackPurchasesPercentage = 0.3F;
        public const decimal OpenWalletUSValue = 0.1M;
        public const int WalletPointsPerUSValue = 30;
        public const int MinTransferPoints = 100;
    }

    public static class MinWithdrawalAmounts
    {
        public const decimal MinUSValue = 7;
        public const decimal MinCostaRicanValue = 2500;
        public const decimal MinMexicanValue = 120;
        public const decimal MinColombianValue = 15000;
    }

    public static class PointsTransferAmountTypes
    {
        public const int Money = 1;
        public const int Points = 2;
    }

    public static class MinTransferAmounts
    {
        public const decimal MinUSValue = 0.5M;
        public const decimal MinCostaRicanValue = 250;
        public const decimal MinMexicanValue = 10;
        public const decimal MinColombianValue = 1500;
    }

    public static class UserInviteRelationConfigValues
    {
        public const decimal JoiningFirstPurchaseMoneyUSValue = 0;
        public const decimal JoiningBonusCommissionMoneyUSValue = 5;
        public const double JoiningBonusCommissionPercentage = 0.6;
        public const decimal FirstLevelAncestorFirstPurchaseMoneyUSValue = 0.25M;
        public const decimal SecondLevelAncestorFirstPurchaseMoneyUSValue = 0;
        public const double FirstLevelAncestorBonusCommissionPercentage = 0.15;
        public const double SecondLevelAncestorBonusCommissionPercentage = 0.05;
        public const int JoiningBonusMonthsLifeSpan = 4;
        public const int AncestorBonusMonthsLifeSpan = -1;
    }

    public static class MembershipPointsOperationTypes
    {
        public const int All = -1;
        public const int PointsBalance = 1;
        public const int TransferPoints = 2;
        public const int ConvertPoints = 3;
    }

    public static class MembershipPointsOperationStatuses
    {
        public const int All = -1;
        public const int Accessible = 1;
        public const int FundingPending = 2;
        public const int ValidationPending = 3;
        public const int InvalidPoints = 4;
        public const int PointsNotFunded = 5;
    }

    public static class MembershipPointsOperationObjectiveTypes
    {
        public const int All = -1;
        public const int NoObjective = 0;
        public const int CommerceLoyalty = 1;
        public const int YOYWallet = 2;
    }

    public static class MembershipPointsOperationReferenceTypes
    {
        public const int All = -1;
        public const int NoRef = 0;//For checkins, invite friends
        public const int Transaction = 1;//For redemptions
        public const int UsageRecordLine = 2;//For claims
        public const int InvitedUser = 3;//For invited friends joined
        public const int MembershipOperation = 4;//For points transfer between users
        public const int MoneyConversionLog = 5;//For points convert to money
        public const int Receipt = 4;//For purchase validations
        public const int MoneyWithdrawal = 5;//For money withdrawals
    }

    public static class MembershipPointsOperationRefIdTypes
    {
        public const int All = -1;
        public const int ProviderMembership = 1;
        public const int BeneficiaryMembership = 2;
        public const int BeneficiaryTenant = 3;
        public const int SourceTenant = 4;
    }

    public static class OperationIssueRefTypes
    {
        public const int All = -1;
        public const int None = 0;
        public const int Transaction = 1;
        public const int Receipt = 2;
    }

    public static class OperationIssueTypes
    {
        public const int All = -1;
        public const int DealDeniel = 1;
        public const int OutOfStockDeal = 2;
        public const int CodeNotVisible = 3;
        public const int InvalidCode = 4;
        public const int CheckTicketManually = 5;
        public const int IncorrectPurchaseAmount = 6;
    }

    public static class OperationIssueStatuses
    {
        public const int All = -1;
        public const int Opened = 1;
        public const int Attending = 2;
        public const int Resolved = 3;
        public const int Closed = 4;
    }

    public static class RewardPointsPerActions
    {
        public const int All = -1;
        public const int JoinLoyaltyClub = 100;
        public const int ClaimRewardPoints = 0;
    }

    public static class CheckInFilters
    {
        public const int BranchId = 1;
        public const int MembershipId = 2;
    }

    public static class BroadcasterStateUpdateTypes
    {
        public const int ActiveState = 1;
        public const int DefaultState = 2;
    }

    public static class TargetParamTypeDefs
    {
        public const int DataType = 1;
        public const int DefinitionType = 2;
        public const int TableName = 3;
        public const int FieldName = 4;
    }

    public static class GeofenceReferenceTypes
    {
        public const int TenantId = 1;
        public const int ZoneId = 2;
    }

    public static class NearbyBroadcastingTypes
    {
        public const int Awareness = 1;
        public const int Marketing = 2;
        public const int Incentive = 3;
    }

    public static class PushNotificationContentTypes
    {
        public const int NoContent = 0;
        public const int BroadcastingContent = 1;
        public const int TransferReceived = 2;
        public const int LoyaltyCheckIn = 3;
        public const int DealClaimed = 4;
        public const int DealRedeemed = 5;
        public const int MoneyConversionProcessed = 6;
        public const int InvitedFriendJoined = 7;
        public const int BirthDate = 8;
    }

    public static class DeviceTypes
    {
        public const int iPhone = 1;
        public const int AndroidPhone = 2;
        public const int WindowsPhone = 3;
        public const int iPad = 4;
        public const int AndroidTablet = 5;
        public const int WindowsTablet = 6;
    }

    public static class SurveyTypes
    {
        public const int All = -1;
        public const int Satisfaction = 1;
        public const int Opinion = 2;
        public const int Discovery = 3;
    }
    public static class SurveyQuestionTypes
    {
        public const int Voting = 1;//Boxes, Clouds
        public const int Rate = 2;//Scale, Faces
        public const int TrueOrFalse = 3;//Thumps
    }

    public static class SurverySequentialType
    {
        public const int Linear = 1;//Doesn't depend on the question previous answer
        public const int Conditional = 2;
    }

    public static class SurveyResponseAllowanceTypes
    {
        public const int UniqueOption = 1;
        public const int MultipleOptions = 2;

    }

    public static class SurveyQuestionOptionsRedentingTypes
    {
        public const int Scale = 1;//Always from 1 to 10
        public const int Faces = 2;//Always to 1 to 5
        public const int Thumps = 3;//It's for true or false
        public const int Box = 4;//It's for unique selection, just 2 options
        public const int Cloud = 5;//It's for unique or multiple selection, up tp 6 options
    }

    public static class SurveyQuestionOptionDefinitionTypes
    {
        public const int Implicit = 1;
        public const int Explicit = 2;

    }

    public static class SurveyAnswerReferences
    {
        public const int ResponseId = 0;
        public const int QuestionId = 1;
    }

    public static class SurveyAnswerDataTypes
    {
        public const int Integer = 0;
        public const int Double = 1;
        public const int String = 2;
    }

    public static class BroadcasterTypes
    {
        public const int All = -1;
        public const int Geofence = 0;
        public const int Signal = 1;
        public const int Audio = 2;
        public const int Beacon = 3;
        public const int Image = 4;
    }

    public static class BroadcastingEventConfidences
    {
        public const int All = -1;
        public const int Low = 0;
        public const int Medium = 1;
        public const int High = 2;
    }

    public static class BroadcastingProtocols
    {
        public const int All = -1;
        public const int SignalRecognition = 1;
        public const int AudioRecognition = 2;
        public const int iBeacon = 3;
        public const int Eddystone = 4;
    }

    public static class CommissionFeeTypes
    {
        public const int All = -1;
        public const int None = 0;
        public const int BasedOnFixedAmount = 1;
        public const int BasedOnIncentive = 2;
        public const int BasedOnAverageTicket = 3;
        public const int BasedOnPurchasedAmount = 4;
    }

    public static class CharityFees
    {
        public const int Percentage = 1;
        public const int FixedAmount = 2;
    }

    public static class CharitySupporterReferences
    {
        public const int Charity = 1;
        public const int Tenant = 2;
    }

    public static class BroadcasterTypeFilters
    {
        public const int NoFilter = -1;
        public const int BeaconType = 1;
        public const int ProtocolType = 2;
        public const int PurposeType = 3;
    }

    public static class CampaignFilters
    {
        public const int All = -1;
        public const int BroadcasterType = 0;
        public const int ContentType = 1;
        public const int ObjectiveType = 2;
    }

    public static class CampaignGenerationType
    {
        public const int All = -1;
        public const int Automatic = 1;
        public const int Manual = 2;
    }



    public static class ArithmeticOps
    {
        public const int Minus = -1;
        public const int Add = 1;
    }


    public static class TenantEnabledCheckInTypes
    {
        public const int None = 0;
        public const int WalkIn = 1;
        public const int Cashier = 2;
        public const int WalkInAndCashier = 3;
    }

    public static class CheckInTypes
    {
        public const int All = -1;
        public const int YOYWalkIn = 1;
        public const int LoyaltyClubWalkIn = 2;
        public const int LoyaltyClubCashier = 3;
    }

    public static class CheckInRefTypes
    {
        public const int All = -1;
        public const int TenantId = 1;
        public const int BranchId = 2;
        public const int UserId = 3;
        public const int BroadcasterId = 4;
        public const int TenantIdPointsGranter = 5;
    }

    public static class CheckInPointsAppliedTypes
    {
        public const int All = -1;
        public const int YOY = 1;
        public const int Commerce = 2;
        public const int SponsoredCommerce = 3;
    }

    public static class CheckInChangeTypes
    {
        public const int All = -1;
        public const int ActiveState = 1;
        public const int UsedForRewardClaim = 2;
    }

    public static class OrderTakingTypes
    {
        public const int InTable = 1;
        public const int InCashier = 2;
        public const int Both = 3;
    }

    public static class MinsToExpires
    {
        public const int Reward = 120;
        public const int CashBack = 120;
        public const int FlashReward = 30;
        public const int Incentive = 5760;
        public const int Offer = 240;
    }

    public static class UserIdentityValueTypes
    {
        public const int PersonalId = 1;
        public const int PhoneNumber = 2;
    }

    public static class UserIdTypes
    {
        public const int Id = 1;
        public const int Username = 2;
        public const int Email = 3;
        public const int FBId = 4;
        public const int AccountNumber = 5;
        public const int AccountCode = 6;
    }


    public static class UserKeys
    {
        public const int Username = 1;
        public const int UserId = 2;
        public const int AccountCode = 3;
        public const int AppleUserId = 4;
    }

    public static class UserInviteRelationHerarchyLevels
    {
        public const int FirstLevelRelation = 1;
        public const int SecondLevelRelation = 2;
    }

    public static class UserEventTypes
    {
        public const int AppOpen = 1;
        public const int OfferRedemption = 2;
    }

    public static class AggregationReferenceTypes
    {
        public const int ParentProduct = 1;
        public const int ParentAggregation = 2;
    }

    public static class CategoryPurposes
    {
        public const int Tenants = 1;
        public const int Products = 2;
    }

    public static class EmployeeSks
    {
        public const int Username = 1;
        public const int AccessKey = 2;
    }

    public static class TimeLimits
    {
        public const int RedemptionCooldownMins = 1;
        public const int ClaimCooldownMins = 1;
        public const int MaxTransactionCreationDaysLapse = 180;
        public const int MaxOfferValidDaysLapse = 120;
        public const int MaxSecurityCodesValidMins = 1440;
        public const int UserAppAccessTokenValidDays = 14;
        public const int BusinessAppAccessTokenValidDays = 3700;
        public const int MinMinutesToRefreshDeals = 5;//300
        public const int MinMinutesToRefreshBroadcastingHistory = 30;//360
        public const int MinMinutesToRefreshClub = 0;//0
        public const int MaxDaysToRedeemRaffle = 15;
        public const int MinCheckInCooldownHours = 12;
        public const int MinCheckInWithoutClaimCooldownDays = 0;// 8;
        public const int MonthsOldTransactionsRecords = 8;
    }

    public static class DistanceLimits
    {
        public const int MaxKMRangeToShowOffers = 15;
        public const int MinKMToRefreshDeals = 15;
        public const int MinKMToRefreshClub = 0;
        public const int MaxKMRangeForMainOffers = 60;
        public const int MaxKMRangeForMainOffersByCountryFactor = 2;
    }

    public static class MainScreenSections
    {
        public const int UserTargettedDeals = 0;
        public const int PopularDeals = 1;
        public const int NearbyDeals = 2;
    }

    public static class ContentDisplayLimits
    {
        public const int MaxTenantsInPreferencesScreen = 42;
        public const int MaxContentCountInCarrousel = 7;
        public const int MaxContentCountInBroadcastingList = 4;
        public const int MaxContentCountByList = 100;
        public const int MaxTenantsCount = 150;
    }

    public static class DisplayImgTypes
    {
        public const int Reward = 1;
        public const int Offer = 2;
        public const int Coupon = 3;
    }

    public static class CommerceKeys
    {
        public const int PrimaryKey = 1;
        public const int TenantKey = 2;
    }

    public static class TenantTypes
    {
        public const int All = -1;
        public const int Commerce = 1;
        public const int Brand = 2;
        public const int Holder = 3;
    }

    public static class TenantInstanceTypes
    {
        public const int All = -1;
        public const int Core = 0;
        public const int Business = 1;
        public const int ShoppingMall = 2;
        public const int Supermarket = 3;
    }

    public static class TenantBusinessStructureTypes
    {
        public const int All = -1;
        public const int BrandHolder = 0;
        public const int Franchises = 1;
        public const int BrandHolderAndFrachises = 2;
    }

    public static class TenantPayerTypes
    {
        public const int FreeService = 0;
        public const int BrandHolder = 1;
        public const int Franchisees = 2;
    }

    public static class TenantReferenceCodeTypes
    {
        public const int None = 0;
        public const int AllPromos = 1;
        public const int ExclusiveOffers = 2;
        public const int ValidReceipts = 3;
    }

    public static class BranchTypes
    {
        public const int Commerce = 1;
        public const int Holder = 2;
    }

    public static class DealClaimMethods
    {
        public const int UserApp = 1;
        public const int CashierApp = 2;
    }


    public static class NearbyMessageTypes
    {
        public const int PushNotification = 1;
        public const int CompleteMessage = 2;
    }

    public static class TenantImgTypes
    {
        public const int Logo = 0;
        public const int LandingImg = 1;
        public const int EmailBackground = 2;
        public const int CarrousedImg = 3;
        public const int WhiteLogo = 4;
    }

    public static class SavingRouteImgTypes
    {
        public const int DisplayImg = 0;
    }

    public static class TransactionReferenceTypes
    {
        public const int NoRef = 0;
        public const int Reward = 1;
        public const int Offer = 2;
        public const int Coupon = 3;
        public const int Special = 4;
        public const int ProductItemInteraction = 5;
        public const int Transaction = 6;
        public const int Receipt = 7;
    }

    public static class PaymentLogReferenceTypes
    {
        public const int All = 0;
        public const int Purchase = 1;
        public const int CashbackIncentive = 2;
    }

    public static class PurchaseCodeTypes
    {
        public const int All = 0;
        public const int AlphaNumeric = 1;
        public const int NumericOnly = 2;
    }


    public static class EarnPointsModalRedirections
    {
        public const int Home = 0;
        public const int Wallet = 1;
        public const int Club = 2;
    }

    public static class LoyaltyTypes
    {
        public const int Wallet = 1;
        public const int Club = 2;
    }

    public static class LoyaltyProgressTypes
    {
        public const int KeepLevel = 1;
        public const int UpgradeLevel = 2;
    }

    public static class EarnedPointsTypes
    {
        public const int None = -1;
        public const int Wallet = 0;
        public const int Club = 1;
        public const int WalletAndClub = 2;
    }

    public static class ContentParamsRefTypes
    {
        public const int Offer = 1;
        public const int BTLContent = 2;
        public const int Survey = 3;
        public const int ProductItem = 4;
    }

    public static class CampaignReferenceTypes
    {
        public const int Offer = 1;
        public const int BTLContent = 2;
    }

    public static class CampaignBroadcasterTypes
    {
        public const int Geofence = 1;
        public const int Beacon = 2;
        public const int Video_Audio = 3;
        public const int ImageRecognition = 4;
        public const int Email = 5;
    }

    public static class DefaultRedemptionMins
    {
        public const int MembershipTransaction = 0;
        public const int Reward = 7200;//5 days
        public const int Offer = 2880;//2 days
        public const int MobileCampaign = 60;
    }

    public static class ObjectiveTypes
    {
        public const int All = 0;
        public const int PointsRedemption = 1;
        public const int RewardPurchases = 2;
        public const int MediaInteraction = 3;
        public const int GenerateTraffic = 4;
        public const int SalesConversion = 5;
        public const int UpSelling = 6;
        public const int ReturningIncentive = 7;
        public const int CrossSelling = 8;
        public const int GenericPurpose = 9;
    }

    public static class BroadcastingTimerByDisplayTypes
    {

        public const int ListingsOnly = 45;
        public const int BroadcastingAndListings = 30;
        public const int BroadcastingOnly = 15;
    }
    public static class MinsToUnlockByObjectiveTypes
    {
        public const int PointsRedemption = 0;
        public const int RewardVisits = 0;
        public const int MediaInteraction = 0;
        public const int SalesConversion = 0;
        public const int GenerateTraffic = 0;
        public const int UpSelling = 0;
        public const int ReturningIncentive = 7200;
        public const int CrossSelling = 0;
        public const int GenericPurpose = 0;
    }

    public static class OfferPurposeTypes
    {
        public const int Reward = 1;
        public const int Deal = 2;
    }

    public static class OfferDisplayPurposeTypes
    {
        public const int Club = 1;
        public const int Listing = 2;
        public const int Broadcasting = 3;
    }

    public static class FileContentTypes
    {
        public const int Blob = 1;
        public const int Url = 2;
    }

    public static class FileTypes
    {
        public const int All = 0;
        public const int Img = 1;
        public const int AnimatedImg = 2;
        public const int Video = 3;
        public const int PDF = 4;
        public const int Document = 5;
    }

    public static class ExternalStorageSources
    {
        public const int All = 0;
        public const int Cloudinary = 1;
        public const int GoogleDrive = 2;
        public const int Dropbox = 3;
    }

    public static class ExternalStorageReferenceTypes
    {
        public const int All = 0;
        public const int BTLContentItem = 1;
        public const int Broadcaster = 2;
    }

    public static class OfferImgTypes
    {
        public const int DisplayImg = 0;
        public const int Code = 1;
    }

    public static class ReceiptImgTypes
    {
        public const int Receipt = 0;
    }

    public static class ExclusiveTypes
    {
        public const int Exclusive = 0;
        public const int NonExclusive = 1;
        public const int All = 2;
    }

    public static class ActiveStates
    {
        public const int Active = 0;
        public const int Inactive = 1;
        public const int All = 2;
    }

    public static class PreferenceInputTypes
    {
        public const int All = 0;
        public const int RadioButton = 1;//Each option has a radio button, 1 single selection
        public const int Checkbox = 2;//Each options has a checkbox, allows multiple selects
        public const int Dropdown = 3;//Options are contained as the options, 1 single selection
        public const int ColorPicker = 4;//User selects from color boxes, allows single select
        public const int TagsPicker = 5;//User selects from a set of tags, allows multiple selects
    }


    public static class MainStates
    {
        public const int Main = 0;
        public const int Others = 1;
        public const int All = 2;
    }

    public static class ValidatedStates
    {
        public const int Validated = 0;
        public const int NotValidated = 1;
        public const int All = 2;
    }

    public static class BlockedStates
    {
        public const int Allowed = 0;
        public const int Blocked = 1;
        public const int All = 2;
    }

    public static class EnabledStates
    {
        public const int Enabled = 0;
        public const int Disabled = 1;
        public const int All = 2;
    }

    public static class ReleaseStates
    {
        public const int Released = 0;
        public const int NotReleased = 1;
        public const int All = 2;
    }

    public static class OpenStates
    {
        public const int Opened = 0;
        public const int Closed = 1;
        public const int All = 2;

    }

    public static class ViewedStates
    {
        public const int All = 0;
        public const int Seen = 1;
        public const int Unseen = 2;

    }

    public static class CompleteStates
    {
        public const int Complete = 0;
        public const int Incomplete = 1;
        public const int All = 2;
    }

    public static class MembershipCategories
    {
        public const int Bronce = 1;
        public const int Silver = 2;
        public const int Gold = 3;
        public const int Platinum = 4;
        public const int Diamond = 5;
    }

    public static class TransactionOrigins
    {
        public const int All = -1;
        public const int Listings = 0;
        public const int ProximityBroadcasting = 1;
        public const int MediaBroadcasting = 2;
        public const int GeolocationBroadcasting = 3;
        public const int WebsiteListings = 4;
        public const int EmailMarketing = 5;
        public const int AppLoyaltyClub = 6;
        public const int SavingRouteAccomplished = 7;
        public const int CheckIn = 8;
        public const int RewardRedemption = 9;
        public const int DealClaim = 10;
        public const int SignUp = 11;
        public const int JoinClub = 12;
        public const int InviteFriend = 13;
        public const int DealRate = 14;
        public const int PointsOperation = 15;
    }

    public static class UserProfileFieldTypes
    {
        public const int BirthDate = 1;
        public const int InvitorReferenceCode = 2;
        public const int PhoneNumber = 3; 
        public const int State = 4;
        public const int ProfilePic = 5;
        public const int Language = 6;
        public const int PersonalId = 7;
    }

    public static class FirstLevelAggregationTypes
    {
        public const int Flavour = 1;
        public const int Color = 2;
    }

    public static class SecondLevelAggregationTypes
    {
        public const int PortionSize = 3;
        public const int Size = 4;
    }

    public static class ThirdLevelAggregationTypes
    {
        public const int Garrison = 5;
        public const int Optional = 6;
        public const int Extra = 7;
    }

    public static class AggregationLevels
    {
        public const int Level1 = 1;
        public const int Level2 = 2;
        public const int Level3 = 3;
    }

    public static class CategoryTypes
    {
        public const int System = 0;
        public const int Regular = 1;
        public const int All = 2;
    }

    public static class CategoryHerarchyLevels
    {
        public const int Preference = 0;
        public const int TenantClassification = 1;
        public const int TenantCategory = 2;
        public const int ProductClassification = 3;
        public const int ProductCategory = 4;
        //public const int ProductWishList = 5;
        public const int HerarchyLevelCount = 5;
    }

    public static class DisplayInCatalogTypes
    {
        public const int Displayed = 0;
        public const int NotDisplayed = 1;
        public const int All = 2;
    }

    public static class BusinessOperationFlags
    {
        public const char GeneratePaymentRequestFromMessagging = '1';
        public const int SetPurchaseAsDispatchedFromMessagging = '5';
    }


    public static class ChangeTypes
    {
        public const int ActiveState = 0;
        public const int ExclusiveState = 1;
        public const int SponsoredState = 2;
        public const int OneTimeClaim = 3;
        public const int ReleasedState = 4;
        public const int Coverage = 5;
        public const int DefaultState = 6;
        public const int HasPreferences = 7;
        public const int MainStatus = 8;
        public const int MandatoryStatus = 9;
        public const int ReplacesImageStatus = 10;
        public const int IsValidState = 11;
        public const int AllowedState = 12;
        public const int CompleterState = 13;
    }

    public static class UsedStates
    {
        public const int Used = 0;
        public const int NotUsed = 1;
        public const int All = 2;

    }

    public static class ExpiredStates
    {
        public const int Expired = 0;
        public const int Valid = 1;
        public const int All = 2;

    }

    public static class PurchaseItemStatuses
    {
        public const int All = 0;
        public const int Placed = 1;
        public const int Payed = 2;
        public const int DispatchValidationRequested = 3;
        public const int Delivered = 4;
        public const int IssueRaised = 5;
        public const int Cancelled = 6;
        public const int Returned = 7;

    }

    public static class PurchaseStatuses
    {
        public const int All = 0;
        public const int Placed = 1;
        public const int Payed = 2;
        public const int PartiallyIssueRaised = 3;
        public const int PartiallyCancelled = 4;
        public const int FullyIssueRaised = 5;
        public const int DispatchValidationRequested = 6;
        public const int Delivered = 7;
        public const int FullyCancelled = 8;

    }

    public static class PurchaseItemRelatedReferences
    {
        public const int All = 0;
        public const int PurchaseId = 1;
        public const int TenantId = 2;
        public const int DispatchBranchId = 3;
        public const int ValidatorSourceId = 4;

    }

    public static class PurchaseCancelledStatuses
    {
        public const int All = 0;
        public const int Cancelled = 1;
        public const int NotCancelled = 2;

    }

    public static class CardFundingTypes
    {
        public const int Debit = 0;
        public const int Credit = 1;

    }
    public static class BankAccTypes
    {
        public const int IndividualAcc = 0;
        public const int EnterpriseAcc = 1;

    }
    public static class InvoiceTypes
    {
        public const int All = 0;
        public const int Invoice = 1;

    }
    public static class InvoiceIdTypes
    {
        public const int All = 0;
        public const int PersonalId = 1;
        public const int LegallId = 2;

    }

    public static class PaymentStatuses
    {
        public const int Failed = -1;
        public const int All = 0;
        public const int Pending = 1;
        public const int OnProcess = 2;
        public const int Completed = 3;

    }

    public static class PaymentRequestStatuses
    {
        public const int DeniedByUser = -2;
        public const int Failed = -1;
        public const int All = 0;
        public const int Created = 1;
        public const int AccesedByUser = 2;
        public const int Completed = 3;

    }

    public static class PaymentRequestIdTypes
    {
        public const int Id = 1;
        public const int LogId = 2;

    }

    public static class PaymentMethods
    {
        public const int CreditCard = 1;
        public const int Money = 2;
    }

    public static class OnlyOnlineTypes
    {
        public const int All = 0;
        public const int OnlineExclusively = 1;
        public const int NotOnlineLimited = 2;
    }

    public static class ProcessingOrderTypes
    {
        public const int All = 0;
        public const int Instantly = 1;
        public const int Scheduled = 2;
    }

    public static class DeliveryMethods
    {
        public const int InStore = 1;
        public const int PickUp = 2;
        public const int Express = 3;
    }


    public static class UserActionTypes
    {
        public const int All = -1;
        public const int ViewODealList = 1;
        public const int ViewDealDetail = 2;
        public const int ViewCatalog = 3;
        public const int ViewImage = 4;
        public const int RedeemDeal = 5;
        public const int FavCatalog = 6;
        public const int ClaimDeal = 7;
        public const int ScoredDeal = 8;
        public const int WriteComment = 9;
        public const int FavCommerce = 10;
        public const int UnFavCommerce = 11;
        public const int ViewReward = 12;
        public const int RedeemReward = 13;
        public const int ClaimReward = 14;
        public const int BroadcastingContentSent = 15;
        public const int OpenBroadcastingContent = 17;
        public const int EnterGeofence = 18;
        public const int ExitGeofence = 19;
        public const int VisitBranch = 20;
        public const int DetectBroadcaster = 21;
        public const int ScanBroadcaster = 22;
        public const int OpenApp = 23;
        public const int OpenEmail = 24;
        public const int ClickEmailButton = 25;
        public const int InviteNewUser = 26;
        public const int AcceptJoinInvitation = 27;
        public const int AnswerSurvey = 28;
    }

    public static class UserActionReferenceTypes
    {
        public const int All = -1;
        public const int NoRef = 0;
        public const int Deal = 1;
        public const int Catalog = 2;
        public const int Reward = 3;
        public const int Image = 4;
        public const int Transaction = 5;
        public const int Category = 6;
        public const int Commerce = 7;
        public const int Branch = 8;
        public const int Campaign = 9;
        public const int Geofence = 10;
        public const int Tone = 11;
        public const int BTL = 12;
        public const int User = 13;
        public const int Survey = 14;
    }

    public static class TransactionTypes
    {
        public const int All = 0;
        public const int AddPoints = 1;
        public const int RedeemDeal = 2;
        public const int AddInvitationPoints = 3;
        public const int PointsRevertion = 4;
        public const int RedeemPoints = 5;
        public const int TransferPoints = 6;
        public const int ClaimDeal = 7;
        public const int RateDeal = 8;
    }

    public static class TicketValidationTypes
    {
        public const int All = 0;
        public const int Redemption = 1;
        public const int CheckIn = 2;
        public const int RedemptionAndCheckIn = 3;
    }

    /// <summary>
    /// This is simply an alias to display the transactions in an order that makes sense to humans
    /// </summary>
    public static class TransactionTypesVisualizer
    {
        public const int All = 0;
        public const int PointsRevertion = 1;
        public const int AddInvitationPoints = 2;
        public const int RateDeal = 3;
        public const int AddPoints = 4;
        public const int TransferPoints = 5;
        public const int RedeemPoints = 6;
        public const int RedeemDeal = 7;
        public const int ClaimDeal = 8;
    }

    public static class MembershipCreationReasonTypes
    {
        public const int All = -1;
        public const int OpenWallet = 1;
        public const int OfferRedemption = 2;
        public const int UserJoin = 3;
        public const int LoyaltyClubCheckIn = 4;
        public const int RewardRedemption = 5;
        public const int PointsTransferReceived = 6;
        public const int EmployeeRoleAssigned = 7;
        public const int WalkInCheckIn = 8;
        public const int PointsConversionAcredited = 9;
    }

    public static class InstallationRefTypes
    {
        public const int Id = 1;
        public const int Username = 2;
        public const int InstallationId = 3;

    }

    public static class ConfirmationTypes
    {
        public const int Email = 0;
        public const int MobilePhone = 1;

    }

    public static class ContestReferenceTypes
    {
        public const int TenantId = 0;
        public const int RewardId = 1;
    }

    public static class RewardOriginatorTypes//This is what originated the reward, this applies to the rewards to award 
    {
        public const int All = -1;
        public const int SavingRoute = 0;
        public const int Redemption = 1;

    }

    public static class UserConfirmationCodeReferenceTypes
    {
        public const int CodeId = 1;
        public const int Code = 2;
        public const int UserId = 3;
        public const int TenantId = 4;
        public const int BranchId = 5;
    }

    public static class UserConfirmationCodeUsageReason
    {
        public const int All = -1;
        public const int MoneyConvert = 1;
        public const int RewardRedemption = 2;
        public const int DealClaim = 3;
    }

    public static class AdDisplayScreenType
    {
        public const int All = -1;
        public const int Totem = 1;
        public const int VerticalScreen = 2;
        public const int HorizontalScreen = 3;
    }

}
