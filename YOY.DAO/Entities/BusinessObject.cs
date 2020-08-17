using System;
using YOY.DAO.Entities.DB;
using YOY.DAO.Entities.DB.Functions;
using YOY.DAO.Entities.DB.StoredProcedures;
using YOY.DAO.Entities.Manager;
using YOY.DAO.Entities.Manager.Misc;
using YOY.DAO.Entities.Manager.Misc.PushNotifications;

namespace YOY.DAO.Entities
{
    // -------------------------------------------------------------------------------------------------------------------------------------------------- //
    // CLASS BUSINESS OBJECT                                                                                                                              //
    // -------------------------------------------------------------------------------------------------------------------------------------------------- //
    /// <summary>
    /// Business Objects is a class that holds all access to application logic that interacts with the resources within the system. The Business Objects
    /// class expose all functionality as properties that dynamically create instances of "managers" which are classes that holds logic for specific resource
    /// in the system.
    /// </summary>
    public class BusinessObjects
    {

        #region PROPERTIES_AND_RESOURCES
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS PRIVATE PROPERTIES AND RESOURCES                                                                                                         //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //

        // DATA CONTEXT --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This class is the database context of the system. Holds all the object representations of the resource implementations within the database and 
        /// manages all the interactions with those resources. The context is a persistent resource proxy. Changes made through this resource persist on the
        /// application lifecycle indefinitely.
        /// </summary>
        private readonly yoyIj7qM58dCjContext _context;
        private StoredProceduresHandler _storedProceduresInvoker;
        private FunctionsHandler _functionsInvoker;

        // TENANT --------------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// The parent tenant is referenced in order to provide reference to its services at the tenante level which are basically file-management and the 
        /// tenant id.
        /// </summary>
        private readonly Tenant _tenant;

        //LOCATIONS MANAGERS -----------------------------------------------------------------------------------------------------------------------------//
        private CountryManager _countryManager;
        private StateManager _stateManager;
        private CityManager _cityManager;
        private DistrictManager _districtManager;

        // APPINSTALLATION MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the app installations
        /// </summary>
        private AppInstallationManager _appInstallationManager;

        // BANKINGINFO MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the business banking infos. Each tenants uses 
        /// </summary>
        private BankingInfoManager _bankingInfoManager;

        // BRANCHES MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the branches. Each tenants uses 
        /// </summary>
        private BranchManager _branchesManager;

        // BRANCH SCHEDULE MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the business branches schedules. Each tenants uses 
        /// </summary>
        private BranchScheduleManager _branchScheduleManager;


        // BROADCASTER MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the broadcasters. Each tenants uses 
        /// </summary>
        private BroadcasterManager _broadcasterManager;

        // BROADCASTING EVENT MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage broadcasting events. Each tenants uses 
        /// </summary>
        private BroadcastingEventManager _broadcastingEvents;

        // MESSAGE SENT MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage messages sent. Each tenants uses 
        /// </summary>
        private BroadcastingLogManager _broadcastingLogManager;

        // BROADCASTING PLAYER LOG MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the business broadcasting schedules. Each tenants uses 
        /// </summary>
        private BroadcastingPlayerLogManager _broadcastingPlayerLogManager;

        // BROADCASTING SCHEDULE MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the business broadcasting schedules. Each tenants uses 
        /// </summary>
        private BroadcastingScheduleManager _broadcastingcheduleManager;

        // BTLCONTENTITEM MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the btl content items. Each tenants uses 
        /// </summary>
        private BTLContentItemManager _btlContentItemManager;

        // BTLCONTENT MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the btl contents. Each tenants uses 
        /// </summary>
        private BTLContentManager _btlContentManager;

        // CASHBACKINCENTIVE MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the cashback incentives. Each tenants uses 
        /// </summary>
        private CashIncentiveManager _cashIncentiveManager;

        // PRODUCT CATEGORY MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the product categories. Each tenants uses 
        /// </summary>
        private CategoryManager _categoryManager;

        // CHECKIN MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the checkIns. Each tenants uses 
        /// </summary>
        private CheckInManager _checkInManager;

        // REWARD MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the rewards. Each tenants uses 
        /// </summary>
        private ClubRewardManager _clubRewardManager;

        // HTTPCALL CONFIG VALUES MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage config values manager. 
        /// </summary>
        private ConfigValuesManager _configValuesManager;

        // CONTENT LOCATION MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the content locations. Each tenants uses 
        /// </summary>
        private ContentLocationManager _contentLocationManager;

        // DELIVERY METHOD MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the delivery methods. Each tenants uses 
        /// </summary>
        private DeliveryMethodManager _deliveryMethodManager;

        // DEPARTMENT MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the floors. Each tenants uses 
        /// </summary>
        private DepartmentManager _departmentManager;

        // EARNINGSINCREASER MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the earnings increaser. Each tenants uses 
        /// </summary>
        private EarningsIncreaserManager _earningsIncreaserManager;

        // EMPLOYEE MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the employees. Each tenants uses 
        /// </summary>
        private EmployeeManager _employeeManager;

        // EXTERNALLY STORED FILE MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to store and retrieve externally stored files from the External Storage. Each tenants uses 
        /// </summary>
        private ExternallyStoredFileManager _externallyStoredFileManager;


        // FEATURED SLIDES MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the featured Slides. Each tenants uses 
        /// </summary>
        private FeaturedSlideManager _featuredSlidesManager;

        // FRANCHISEE MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the business franchisees. Each tenants uses 
        /// </summary>
        private FranchiseeManager _franchiseeManager;

        // GEOFENCE MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage geofences. Each tenants uses 
        /// </summary>
        private GeofenceManager _geofenceManager;

        // GEOTRIGGER MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage geotriggers. Each tenants uses 
        /// </summary>
        private GeotriggerManager _geotriggers;

        // GEOZONE MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage geozones. Each tenants uses 
        /// </summary>
        private GeozoneManager _geozones;

        // HARDWAREIOTDEVICE MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the hardware IOT devices. Each tenants uses 
        /// </summary>
        private HardwareIOTDeviceManager _hardwareIOTDeviceManager;


        // HTTPCALL INVOKATION LOG MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage httpcall invokation manager. Each tenants uses 
        /// </summary>
        private HttpcallInvokationLogManager _httpcallInvokationLogManager;

        // IMAGE MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to store and retrieve images from the External Storage. Each tenants uses 
        /// </summary>
        private ImageManager _imageManager;

        // INVOICING INFO MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage invoicing info manager. Each tenants uses 
        /// </summary>
        private InvoicingInfoManager _invoicingInfoManager;

        // KEYWORD MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the keywords. Each tenants uses 
        /// </summary>
        private KeywordManager _keywordManager;

        // MEMBERSHIPLEVEL MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the membership level. Each tenants uses 
        /// </summary>
        private MembershipLevelManager _membershipLevelManager;

        // MEMBERSHIP MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the membership. Each tenants uses 
        /// </summary>
        private MembershipManager _membershipManager;

        // MONETARYFEELOGMANAGER MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the monetary fee logs and their items. Each tenants uses 
        /// </summary>
        private MonetaryFeeLogManager _monetaryFeeLogManager;

        // MONEYCONVERSIONLOGMANAGER MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the money conversion logs and their items. Each tenants uses 
        /// </summary>
        private MoneyConversionLogManager _moneyConversionLogManager;

        // MONEYDRAWALS MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the money withdrawals and their items. Each tenants uses 
        /// </summary>
        private MoneyWithdrawalManager _moneyWithDrawalManager;

        // OFFER MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the offers. Each tenants uses 
        /// </summary>
        private OfferManager _offerManager;

        // OFFER PREFERENCE MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the offer preferences. Each tenants uses 
        /// </summary>
        private OfferPreferenceManager _offerPreferenceManager;

        // OPERATION ISSUE MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the business operation issues. Each tenants uses 
        /// </summary>
        private OperationIssueManager _operationIssueManager;

        // OPERATION FLOW STEP MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the business operation flow steps. Each tenants uses 
        /// </summary>
        private OperationFlowStepLogManager _operationFlowStepManager;

        // PAYMENT INFO MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the payment infos. Each tenants uses 
        /// </summary>
        private PaymentInfoManager _paymentInfoManager;

        // PAYMENT LOG MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the payment logs. Each tenants uses 
        /// </summary>
        private PaymentLogManager _paymentLogManager;

        // PAYMENT METHOD MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the payment methods. Each tenants uses 
        /// </summary>
        private PaymentMethodManager _paymentMethodManager;

        // PAYMENT REQUEST MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the payment requests. Each tenants uses 
        /// </summary>
        private PaymentRequestManager _paymentRequestManager;

        // PURCHASE LOG MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the purchase logs. Each tenants uses 
        /// </summary>
        private PurchaseManager _purchaseManager;

        // RECEIPTANALYZERCONFIGMANAGER MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the receipt analyzer config. Each tenants uses 
        /// </summary>
        private ReceiptAnalyzerConfigManager _receiptAnalyzerConfigManager;

        // RECEIPTMANAGER MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the receipt and their items. Each tenants uses 
        /// </summary>
        private ReceiptManager _receiptManager;

        // REWARD MANAGER MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the reward manager. Each tenants uses 
        /// </summary>
        private RewardManager _rewardManager;

        // SAVED ITEMS MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the saved items. Each tenants uses 
        /// </summary>
        private SavedItemManager _savedItems;

        // SEARCHABLE MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the searables objects. Each tenants uses 
        /// </summary>
        private SearchableManager _searchableManager;

        // SEARCH LOGS MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the search logs. Each tenants uses 
        /// </summary>
        private SearchLogManager _searchLogManager;

        // SHOPPING CART ITEMS MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the shopping cart items items. Each tenants uses 
        /// </summary>
        private ShoppingCartItemManager _shoppingCartItems;

        // TEXT MESSAGE LOGS MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the text message logs. Each tenants uses 
        /// </summary>
        private TextMessageLogManager _textMsgLogs;

        //TENANT MANAGERS
        private TenantManager _tenantManager;

        // TRANSACTION LOCATION MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the membership transactions. Each tenants uses 
        /// </summary>
        private TransactionLocationManager _transactionLocationManager;


        // TRANSACTION MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the transactions. Each tenants uses 
        /// </summary>
        private TransactionManager _transactionManager;

        // USER INTEREST FACTOR MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage user interest factors. Each tenants uses 
        /// </summary>
        private UserInterestFactorManager _userInterestFactorManager;

        // USER INTEREST MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage user interests. Each tenants uses 
        /// </summary>
        private UserInterestManager _userInterestManager;

        // USERINVITERELATION MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the user invite relations. Each tenants uses 
        /// </summary>
        private UserInviteRelationManager _userInviteRelationManager;

        // USER MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the users. Each tenants uses 
        /// </summary>
        private UserManager _userManager;

        // VISITOR LOG MANAGER --------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property provides functionality to manage the visitor logs. Each tenants uses 
        /// </summary>
        private VisitorLogManager _visitorLogManager;

        #endregion

        #region DYNAMIC_LOADERS

        /// <summary>
        /// The BusinessObjects can only be instantiated using a factory method that retrieves the correct instance of business objects
        /// </summary>
        /// <param name="tenant"></param>
        /// <returns></returns>
        public StoredProceduresHandler StoredProcsHandler
        {
            get
            {
                if (_storedProceduresInvoker != null)
                {
                    return _storedProceduresInvoker;
                }
                else
                {
                    return _storedProceduresInvoker = new StoredProceduresHandler(this.Context);
                }
            }

        } // METHOD STOREDPROCSHANDLER ENDS ------------------------------------------------------------------------------------------------------------------- //


        /// <summary>
        /// The BusinessObjects can only be instantiated using a factory method that retrieves the correct instance of business objects
        /// </summary>
        /// <param name="tenant"></param>
        /// <returns></returns>
        public FunctionsHandler FuncsHandler
        {
            get
            {
                if (_functionsInvoker != null)
                {
                    return _functionsInvoker;
                }
                else
                {
                    return _functionsInvoker = new FunctionsHandler(this.Context);
                }
            }

        } // METHOD FUNCSHANDLER ENDS ------------------------------------------------------------------------------------------------------------------- //


        // RESOURCES:/ LOCATIONS ---------------------------------------------------------------------------------------------------------------------------- //

        /// <summary>
        /// This dynamic loader exposes the Countries resource
        /// </summary>
        public CountryManager Countries
        {
            get
            {
                if (this._countryManager != null)
                    return _countryManager;
                else
                {
                    this._countryManager = new CountryManager(this);
                    return _countryManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY COUNTRIES ENDS ------------------------------------------------------------------------------------------------------------------------ //

        /// <summary>
        /// This dynamic loader exposes the States resource
        /// </summary>
        public StateManager States
        {
            get
            {
                if (this._stateManager != null)
                    return _stateManager;
                else
                {
                    this._stateManager = new StateManager(this);
                    return _stateManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY STATES ENDS ------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// This dynamic loader exposes the Cities resource
        /// </summary>
        public CityManager Cities
        {
            get
            {
                if (this._cityManager != null)
                    return _cityManager;
                else
                {
                    this._cityManager = new CityManager(this);
                    return _cityManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY CITIES ENDS ------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// This dynamic loader exposes the Districts resource
        /// </summary>
        public DistrictManager Districts
        {
            get
            {
                if (this._districtManager != null)
                    return _districtManager;
                else
                {
                    this._districtManager = new DistrictManager(this);
                    return _districtManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY DISTRICTS ENDS ------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// This dynamic loader exposes the app installation resource
        /// </summary>
        public AppInstallationManager AppInstallations
        {
            get
            {
                if (this._appInstallationManager != null)
                    return _appInstallationManager;
                else
                {
                    this._appInstallationManager = new AppInstallationManager(this);
                    return _appInstallationManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY APP INSTALLATIONS ENDS ------------------------------------------------------------------------------------------------------------------------ //



        // RESOURCES:/ BANKINGINFOS ---------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This dynamic loader exposes the Banking Info resource
        /// </summary>
        public BankingInfoManager BankingInfos
        {
            get
            {
                if (this._bankingInfoManager != null)
                    return _bankingInfoManager;
                else
                {
                    this._bankingInfoManager = new BankingInfoManager(this);
                    return _bankingInfoManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY BANKINGINFOS ENDS ------------------------------------------------------------------------------------------------------------------------ //


        // RESOURCES:/ BRANCHES ---------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This dynamic loader exposes the Branches for the current tenant context.
        /// </summary>
        public BranchManager Branches
        {
            get
            {
                if (this._branchesManager != null)
                    return _branchesManager;
                else
                {
                    this._branchesManager = new BranchManager(this);
                    return _branchesManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY BRANCHES ENDS ------------------------------------------------------------------------------------------------------------------------ //


        // RESOURCES:/ BRANCHSCHEDULES ---------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This dynamic loader exposes the Branch Schedules resource
        /// </summary>
        public BranchScheduleManager BranchSchedules
        {
            get
            {
                if (this._branchScheduleManager != null)
                    return _branchScheduleManager;
                else
                {
                    this._branchScheduleManager = new BranchScheduleManager(this);
                    return _branchScheduleManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY BRANCHSCHEDULES ENDS ------------------------------------------------------------------------------------------------------------------------ //

        /// <summary>
        /// This dynamic loader exposes the broadcaster resource
        /// </summary>
        public BroadcasterManager Broadcasters
        {
            get
            {
                if (this._broadcasterManager != null)
                    return _broadcasterManager;
                else
                {
                    this._broadcasterManager = new BroadcasterManager(this);
                    return _broadcasterManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY BROADCASTERS ENDS ------------------------------------------------------------------------------------------------------------------------ //

        /// <summary>
        /// This dynamic loader exposes the broadcasting events resource
        /// </summary>
        public BroadcastingEventManager BroadcastingEvents
        {
            get
            {
                if (this._broadcastingEvents != null)
                    return _broadcastingEvents;
                else
                {
                    this._broadcastingEvents = new BroadcastingEventManager(this);
                    return _broadcastingEvents;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY BROADCASTING EVENTS ENDS ------------------------------------------------------------------------------------------------------------------------ //

        /// <summary>
        /// This dynamic loader exposes the broadcasting messages sent resource
        /// </summary>
        public BroadcastingLogManager BroadcastingLogs
        {
            get
            {
                if (this._broadcastingLogManager != null)
                    return _broadcastingLogManager;
                else
                {
                    this._broadcastingLogManager = new BroadcastingLogManager(this);
                    return _broadcastingLogManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY MESSAGES LOG ENDS ------------------------------------------------------------------------------------------------------------------------ //


        // RESOURCES:/ BROADCASTINGPLAYERLOG ---------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This dynamic loader exposes the Branch Schedules resource
        /// </summary>
        public BroadcastingPlayerLogManager BroadcastingPlayerLogs
        {
            get
            {
                if (this._broadcastingPlayerLogManager != null)
                    return _broadcastingPlayerLogManager;
                else
                {
                    this._broadcastingPlayerLogManager = new BroadcastingPlayerLogManager(this);
                    return _broadcastingPlayerLogManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY BRANCHSCHEDULES ENDS ------------------------------------------------------------------------------------------------------------------------ //

        // RESOURCES:/ BROADCASTINGSCHEDULES ---------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This dynamic loader exposes the Branch Schedules resource
        /// </summary>
        public BroadcastingScheduleManager BroadcastingSchedules
        {
            get
            {
                if (this._broadcastingcheduleManager != null)
                    return _broadcastingcheduleManager;
                else
                {
                    this._broadcastingcheduleManager = new BroadcastingScheduleManager(this);
                    return _broadcastingcheduleManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY BRANCHSCHEDULES ENDS ------------------------------------------------------------------------------------------------------------------------ //

        /// <summary>
        /// This dynamic loader exposes the  btl content resource
        /// </summary>
        public BTLContentItemManager BTLContentItems
        {
            get
            {
                if (this._btlContentItemManager != null)
                    return _btlContentItemManager;
                else
                {
                    this._btlContentItemManager = new BTLContentItemManager(this);
                    return _btlContentItemManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY BTLCONTENTITEMS ENDS ------------------------------------------------------------------------------------------------------------------------ //

        /// <summary>
        /// This dynamic loader exposes the  btl content resource
        /// </summary>
        public BTLContentManager BTLContents
        {
            get
            {
                if (this._btlContentManager != null)
                    return _btlContentManager;
                else
                {
                    this._btlContentManager = new BTLContentManager(this);
                    return _btlContentManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY BTLCONTENTS ENDS ------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// This dynamic loader exposes the  cashback incentive resource
        /// </summary>
        public CashIncentiveManager CashIncentives
        {
            get
            {
                if (this._cashIncentiveManager != null)
                    return _cashIncentiveManager;
                else
                {
                    this._cashIncentiveManager = new CashIncentiveManager(this);
                    return _cashIncentiveManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY CASHBACK INCENTIVES ENDS ------------------------------------------------------------------------------------------------------------------------ //


        // RESOURCES:/ PRODUCTCATEGORIES ---------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This dynamic loader exposes the Categories resource
        /// </summary>
        public CategoryManager Categories
        {
            get
            {
                if (this._categoryManager != null)
                    return _categoryManager;
                else
                {
                    this._categoryManager = new CategoryManager(this);
                    return _categoryManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY PRODUCTCATEGORIES ENDS ------------------------------------------------------------------------------------------------------------------------ //



        // RESOURCES:/ MEMBERSHIPLEVELS ---------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This dynamic loader exposes the MembershiLevels resource
        /// </summary>
        public CheckInManager CheckIns
        {
            get
            {
                if (this._checkInManager != null)
                    return _checkInManager;
                else
                {
                    this._checkInManager = new CheckInManager(this);
                    return _checkInManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY MEMBERSHIPLEVELS ENDS ------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// This dynamic loader exposes the  reward resource
        /// </summary>
        public ClubRewardManager Rewards
        {
            get
            {
                if (this._clubRewardManager != null)
                    return _clubRewardManager;
                else
                {
                    this._clubRewardManager = new ClubRewardManager(this);
                    return _clubRewardManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY CLUB REWARD ENDS ------------------------------------------------------------------------------------------------------------------------ //

        /// <summary>
        /// This dynamic loader exposes the config values resource
        /// </summary>
        public ConfigValuesManager ConfigValues
        {
            get
            {
                if (this._configValuesManager != null)
                    return _configValuesManager;
                else
                {
                    this._configValuesManager = new ConfigValuesManager(this);
                    return _configValuesManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY CONFIG VALUES ENDS -------------------------------------------------------------------------------------------------------- //


        // RESOURCES:/ CONTENTLOCATIONS ---------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This dynamic loader exposes the Branches resource
        /// </summary>
        public ContentLocationManager ContentLocations
        {
            get
            {
                if (this._contentLocationManager != null)
                    return _contentLocationManager;
                else
                {
                    this._contentLocationManager = new ContentLocationManager(this);
                    return _contentLocationManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY CONTENT LOCATION MANAGER ENDS ------------------------------------------------------------------------------------------------------------------------ //


        // RESOURCES:/ DELIVERYMETHODS ---------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This dynamic loader exposes the Delivery Methods resource
        /// </summary>
        public DeliveryMethodManager DeliveryMethods
        {
            get
            {
                if (this._deliveryMethodManager != null)
                    return _deliveryMethodManager;
                else
                {
                    this._deliveryMethodManager = new DeliveryMethodManager(this);
                    return _deliveryMethodManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY DELIVERYMETHODS ENDS ------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// This dynamic loader exposes the Department resource
        /// </summary>
        public DepartmentManager Departments
        {
            get
            {
                if (this._departmentManager != null)
                    return _departmentManager;
                else
                {
                    this._departmentManager = new DepartmentManager(this);
                    return _departmentManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY FLOORS ENDS ------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// This dynamic loader exposes the  earnings increaser resource
        /// </summary>
        public EarningsIncreaserManager EarningsIncreaser
        {
            get
            {
                if (this._earningsIncreaserManager != null)
                    return _earningsIncreaserManager;
                else
                {
                    this._earningsIncreaserManager = new EarningsIncreaserManager(this);
                    return _earningsIncreaserManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY OFFERS ENDS ------------------------------------------------------------------------------------------------------------------------ //


        // RESOURCES:/ EMPLOYESS ---------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This dynamic loader exposes the Employees for the current tenant context.
        /// </summary>
        public EmployeeManager Employees
        {
            get
            {
                if (this._employeeManager != null)
                    return _employeeManager;
                else
                {
                    this._employeeManager = new EmployeeManager(this);
                    return _employeeManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY EMPLOYEES ENDS ------------------------------------------------------------------------------------------------------------------------ //


        // RESOURCES:/ EXTERNALLY STORED FILES ---------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This dynamic loader exposes the ExternallyStoredFiles resource that allows saving and retrieving files for the current tenant context.
        /// </summary>
        public ExternallyStoredFileManager ExternallyStoredFiles
        {
            get
            {
                if (this._externallyStoredFileManager != null)
                    return _externallyStoredFileManager;
                else
                {
                    this._externallyStoredFileManager = new ExternallyStoredFileManager(this);
                    return _externallyStoredFileManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY EXTERNALLYSTOREDFILES ENDS ------------------------------------------------------------------------------------------------------------------------ //


        // RESOURCES:/ FEATUREDSLIDES ---------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This dynamic loader exposes the Featured Slides resource
        /// </summary>
        public FeaturedSlideManager FeaturedSlides
        {
            get
            {
                if (this._featuredSlidesManager != null)
                    return _featuredSlidesManager;
                else
                {
                    this._featuredSlidesManager = new FeaturedSlideManager(this);
                    return _featuredSlidesManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY FEATUREDSLIDES ENDS ------------------------------------------------------------------------------------------------------------------------ //


        // RESOURCES:/ FRANCHISEE ---------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This dynamic loader exposes the Franchisees resource
        /// </summary>
        public FranchiseeManager Franchisees
        {
            get
            {
                if (this._franchiseeManager != null)
                    return _franchiseeManager;
                else
                {
                    this._franchiseeManager = new FranchiseeManager(this);
                    return _franchiseeManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY FRANCHISEE ENDS ------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// This dynamic loader exposes the geofences resource
        /// </summary>
        public GeofenceManager Geofences
        {
            get
            {
                if (this._geofenceManager != null)
                    return _geofenceManager;
                else
                {
                    this._geofenceManager = new GeofenceManager(this);
                    return _geofenceManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY GEOFENCES ENDS ------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// This dynamic loader exposes the geotriggers resource
        /// </summary>
        public GeotriggerManager Geotriggers
        {
            get
            {
                if (this._geotriggers != null)
                    return _geotriggers;
                else
                {
                    this._geotriggers = new GeotriggerManager(this);
                    return _geotriggers;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY GEOFENCES ENDS ------------------------------------------------------------------------------------------------------------------------ //

        /// <summary>
        /// This dynamic loader exposes the geozones resource
        /// </summary>
        public GeozoneManager Geozones
        {
            get
            {
                if (this._geozones != null)
                    return _geozones;
                else
                {
                    this._geozones = new GeozoneManager(this);
                    return _geozones;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY GEOFENCES ENDS ------------------------------------------------------------------------------------------------------------------------ //

        /// <summary>
        /// This dynamic loader exposes the hardware IOT device resource
        /// </summary>
        public HardwareIOTDeviceManager HardwareDevices
        {
            get
            {
                if (this._hardwareIOTDeviceManager != null)
                    return _hardwareIOTDeviceManager;
                else
                {
                    this._hardwareIOTDeviceManager = new HardwareIOTDeviceManager(this);
                    return _hardwareIOTDeviceManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY HARWAREDEVICES ENDS ------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// This dynamic loader exposes the httpcall invokations resource
        /// </summary>
        public HttpcallInvokationLogManager HttpcallInvokationLogs
        {
            get
            {
                if (this._httpcallInvokationLogManager != null)
                    return _httpcallInvokationLogManager;
                else
                {
                    this._httpcallInvokationLogManager = new HttpcallInvokationLogManager(this);
                    return _httpcallInvokationLogManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY HTTPCALL INVOCATIONS ENDS -------------------------------------------------------------------------------------------------------- //


        // RESOURCES:/ IMAGES ---------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This dynamic loader exposes the Images resource that allows saving and retrieving files for the current tenant context.
        /// </summary>
        public ImageManager Images
        {
            get
            {
                if (this._imageManager != null)
                    return _imageManager;
                else
                {
                    this._imageManager = new ImageManager(this);
                    return _imageManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY IMAGES ENDS ------------------------------------------------------------------------------------------------------------------------ //


        // RESOURCES:/ IMAGES ---------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This dynamic loader exposes the Images resource that allows maning the invoicing infos for the current tenant context.
        /// </summary>
        public InvoicingInfoManager InvoicingInfos
        {
            get
            {
                if (this._invoicingInfoManager != null)
                    return _invoicingInfoManager;
                else
                {
                    this._invoicingInfoManager = new InvoicingInfoManager(this);
                    return _invoicingInfoManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY INVOICING INFOS ENDS ------------------------------------------------------------------------------------------------------------------------ //


        // RESOURCES: KEYWORDS ---------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This dynamic loader exposes the Keywords resource
        /// </summary>
        public KeywordManager Keywords
        {
            get
            {
                if (this._keywordManager != null)
                    return _keywordManager;
                else
                {
                    this._keywordManager = new KeywordManager(this);
                    return _keywordManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY KEYWORDS ENDS ------------------------------------------------------------------------------------------------------------------------ //


        // RESOURCES:/ MEMBERSHIPLEVELS ---------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This dynamic loader exposes the MembershiLevels resource
        /// </summary>
        public MembershipLevelManager MembershipLevels
        {
            get
            {
                if (this._membershipLevelManager != null)
                    return _membershipLevelManager;
                else
                {
                    this._membershipLevelManager = new MembershipLevelManager(this);
                    return _membershipLevelManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY MEMBERSHIPLEVELS ENDS ------------------------------------------------------------------------------------------------------------------------ //


        // RESOURCES:/ MEMBERSHIPS ---------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This dynamic loader exposes the Membershis resource
        /// </summary>
        public MembershipManager Memberships
        {
            get
            {
                if (this._membershipManager != null)
                    return _membershipManager;
                else
                {
                    this._membershipManager = new MembershipManager(this);
                    return _membershipManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY MEMBERSHIPS ENDS ------------------------------------------------------------------------------------------------------------------------ //


        // RESOURCES: MONETARYFEELOG ---------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This dynamic loader exposes the MonetaryFeeLog resource
        /// </summary>
        public MonetaryFeeLogManager MonetaryFeeLogs
        {
            get
            {
                if (this._monetaryFeeLogManager != null)
                    return _monetaryFeeLogManager;
                else
                {
                    this._monetaryFeeLogManager = new MonetaryFeeLogManager(this);
                    return _monetaryFeeLogManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY MONETARYFEELOGS ENDS ------------------------------------------------------------------------------------------------------------------------ //

        // RESOURCES: MONEYCONVERSIONLOG ---------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This dynamic loader exposes the MoneyConversionLog resource
        /// </summary>
        public MoneyConversionLogManager MoneyConversionLogs
        {
            get
            {
                if (this._moneyConversionLogManager != null)
                    return _moneyConversionLogManager;
                else
                {
                    this._moneyConversionLogManager = new MoneyConversionLogManager(this);
                    return _moneyConversionLogManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY MONEYCONVERSIONLOGS ENDS ------------------------------------------------------------------------------------------------------------------------ //

        // RESOURCES: CASHWITHDRAWALS ---------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This dynamic loader exposes the MonetaryFeeLog resource
        /// </summary>
        public MoneyWithdrawalManager MoneyWithdrawals
        {
            get
            {
                if (this._moneyWithDrawalManager != null)
                    return _moneyWithDrawalManager;
                else
                {
                    this._moneyWithDrawalManager = new MoneyWithdrawalManager(this);
                    return _moneyWithDrawalManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY CASHWITHDRAWALS ENDS ------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// This dynamic loader exposes the  offer resource
        /// </summary>
        public OfferManager Offers
        {
            get
            {
                if (this._offerManager != null)
                    return _offerManager;
                else
                {
                    this._offerManager = new OfferManager(this);
                    return _offerManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY OFFERS ENDS ------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// This dynamic loader exposes the  offer resource
        /// </summary>
        public OfferPreferenceManager OfferPreferences
        {
            get
            {
                if (this._offerPreferenceManager != null)
                    return _offerPreferenceManager;
                else
                {
                    this._offerPreferenceManager = new OfferPreferenceManager(this);
                    return _offerPreferenceManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY OFFER PREFERENCES ENDS ------------------------------------------------------------------------------------------------------------------------ //


        // RESOURCES:/ OPERATION ISSUE ---------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This dynamic loader exposes the Operation Issues resource
        /// </summary>
        public OperationIssueManager OperationIssues
        {
            get
            {
                if (this._operationIssueManager != null)
                    return _operationIssueManager;
                else
                {
                    this._operationIssueManager = new OperationIssueManager(this);
                    return _operationIssueManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY OPERATIONISSUES ENDS ------------------------------------------------------------------------------------------------------------------------ //


        // RESOURCES:/ OPERATION ISSUE ---------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This dynamic loader exposes the Operation Flow Steps resource
        /// </summary>
        public OperationFlowStepLogManager OperationFlowSteps
        {
            get
            {
                if (this._operationFlowStepManager != null)
                    return _operationFlowStepManager;
                else
                {
                    this._operationFlowStepManager = new OperationFlowStepLogManager(this);
                    return _operationFlowStepManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY OPERATIONFLOWSTEPLOG ENDS ------------------------------------------------------------------------------------------------------------------------ //


        // RESOURCES:/ PAYMENTINFOS ---------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This dynamic loader exposes the Payment Infos resource
        /// </summary>
        public PaymentInfoManager PaymentInfos
        {
            get
            {
                if (this._paymentInfoManager != null)
                    return _paymentInfoManager;
                else
                {
                    this._paymentInfoManager = new PaymentInfoManager(this);
                    return _paymentInfoManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY PAYMENTINFOS ENDS ------------------------------------------------------------------------------------------------------------------------ //


        // RESOURCES:/ PAYMENTILOGS ---------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This dynamic loader exposes the Payment Logs resource
        /// </summary>
        public PaymentLogManager PaymentLogs
        {
            get
            {
                if (this._paymentLogManager != null)
                    return _paymentLogManager;
                else
                {
                    this._paymentLogManager = new PaymentLogManager(this);
                    return _paymentLogManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY PAYMENTLOGS ENDS ------------------------------------------------------------------------------------------------------------------------ //


        // RESOURCES:/ PAYMENTIREQUESTS ---------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This dynamic loader exposes the Payment Requests resource
        /// </summary>
        public PaymentRequestManager PaymentRequests
        {
            get
            {
                if (this._paymentRequestManager != null)
                    return _paymentRequestManager;
                else
                {
                    this._paymentRequestManager = new PaymentRequestManager(this);
                    return _paymentRequestManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY PAYMENTREQUESTS ENDS ------------------------------------------------------------------------------------------------------------------------ //


        // RESOURCES:/ PAYMENTMETHODS ---------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This dynamic loader exposes the Payment Methods resource
        /// </summary>
        public PaymentMethodManager PaymentMethods
        {
            get
            {
                if (this._paymentMethodManager != null)
                    return _paymentMethodManager;
                else
                {
                    this._paymentMethodManager = new PaymentMethodManager(this);
                    return _paymentMethodManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY PAYMENTMETHODS ENDS ------------------------------------------------------------------------------------------------------------------------ //


        // RESOURCES:/ PURCHASELOGS ---------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This dynamic loader exposes the Pruchase Logs resource
        /// </summary>
        public PurchaseManager Purchases
        {
            get
            {
                if (this._purchaseManager != null)
                    return _purchaseManager;
                else
                {
                    this._purchaseManager = new PurchaseManager(this);
                    return _purchaseManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY PURCHASES ENDS ------------------------------------------------------------------------------------------------------------------------ //


        // RESOURCES: RECEIPT ANALYZER ---------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This dynamic loader exposes the Receipt Analyzer Config resource
        /// </summary>
        public ReceiptAnalyzerConfigManager ReceiptAnalyzerConfigs
        {
            get
            {
                if (this._receiptAnalyzerConfigManager != null)
                    return _receiptAnalyzerConfigManager;
                else
                {
                    this._receiptAnalyzerConfigManager = new ReceiptAnalyzerConfigManager(this);
                    return _receiptAnalyzerConfigManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY RECEIPT ANALYZER CONFIG ENDS ------------------------------------------------------------------------------------------------------------------------ //


        // RESOURCES: RECEIPT ---------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This dynamic loader exposes the Receipt resource
        /// </summary>
        public ReceiptManager Receipts
        {
            get
            {
                if (this._receiptManager != null)
                    return _receiptManager;
                else
                {
                    this._receiptManager = new ReceiptManager(this);
                    return _receiptManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY RECEIPT ENDS ------------------------------------------------------------------------------------------------------------------------ //

        /// <summary>
        /// This dynamic loader exposes the loyalty rewards resource
        /// </summary>
        public RewardManager LoyaltyRewards
        {
            get
            {
                if (this._rewardManager != null)
                    return _rewardManager;
                else
                {
                    this._rewardManager = new RewardManager(this);
                    return _rewardManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY LOYALTY REWARDS ENDS ------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// This dynamic loader exposes the saved items resource
        /// </summary>
        public SavedItemManager SavedItems
        {
            get
            {
                if (this._savedItems != null)
                    return _savedItems;
                else
                {
                    this._savedItems = new SavedItemManager(this);
                    return _savedItems;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY SAVED ITEMS ENDS ------------------------------------------------------------------------------------------------------------------------ //


        // RESOURCES:/ SEARCHABLES ---------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This dynamic loader exposes the Searchable resource
        /// </summary>
        public SearchableManager Searchables
        {
            get
            {
                if (this._searchableManager != null)
                    return _searchableManager;
                else
                {
                    this._searchableManager = new SearchableManager(this);
                    return _searchableManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY SEARCHABLES ENDS ------------------------------------------------------------------------------------------------------------------------ //


        // RESOURCES:/ SEARCHLOGS ---------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This dynamic loader exposes the Search Logs resource
        /// </summary>
        public SearchLogManager SearchLogs
        {
            get
            {
                if (this._searchLogManager != null)
                    return _searchLogManager;
                else
                {
                    this._searchLogManager = new SearchLogManager(this);
                    return _searchLogManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY SEARCHLOGS ENDS ------------------------------------------------------------------------------------------------------------------------ //


        // RESOURCES:/ SHOPPINGCARTITEMS ---------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This dynamic loader exposes the Shopping Cart Items resource
        /// </summary>
        public ShoppingCartItemManager ShoppingCartItems
        {
            get
            {
                if (this._shoppingCartItems != null)
                    return _shoppingCartItems;
                else
                {
                    this._shoppingCartItems = new ShoppingCartItemManager(this);
                    return _shoppingCartItems;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY SHOPPINGCARTITEMS ENDS ------------------------------------------------------------------------------------------------------------------------ //


        // RESOURCES:/ SEARCHLOGS ---------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This dynamic loader exposes the SMS Logs resource
        /// </summary>
        public TextMessageLogManager TextMsgLogs
        {
            get
            {
                if (this._textMsgLogs != null)
                    return _textMsgLogs;
                else
                {
                    this._textMsgLogs = new TextMessageLogManager(this);
                    return _textMsgLogs;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY SMSLOGS ENDS ------------------------------------------------------------------------------------------------------------------------ //



        /// <summary>
        /// This dynamic loader exposes the TenantInfo resource
        /// </summary>
        public TenantManager Commerces
        {
            get
            {
                if (this._tenantManager != null)
                    return _tenantManager;
                else
                {
                    this._tenantManager = new TenantManager(this);
                    return _tenantManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY MARKETING PLANS ENDS ------------------------------------------------------------------------------------------------------------------------ //



        // RESOURCES:/ TRANSACTIONLOCATIONS ---------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This dynamic loader exposes the TransactionLocations resource
        /// </summary>
        public TransactionLocationManager TransactionLocations
        {
            get
            {
                if (this._transactionLocationManager != null)
                    return _transactionLocationManager;
                else
                {
                    this._transactionLocationManager = new TransactionLocationManager(this);
                    return _transactionLocationManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY TRANSACTIONLOCATION ENDS ------------------------------------------------------------------------------------------------------------------------ //


        // RESOURCES:/ TRANSACTIONS ---------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This dynamic loader exposes the Transactions resource
        /// </summary>
        public TransactionManager Transactions
        {
            get
            {
                if (this._transactionManager != null)
                    return _transactionManager;
                else
                {
                    this._transactionManager = new TransactionManager(this);
                    return _transactionManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY TRANSACTION ENDS ------------------------------------------------------------------------------------------------------------------------ //


        /// <summary>
        /// This dynamic loader exposes the user interest relevances resource
        /// </summary>
        public UserInterestFactorManager UserInterestFactors
        {
            get
            {
                if (this._userInterestFactorManager != null)
                    return _userInterestFactorManager;
                else
                {
                    this._userInterestFactorManager = new UserInterestFactorManager(this);
                    return _userInterestFactorManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY USERINTERESTS ENDS ------------------------------------------------------------------------------------------------------------------------ //



        /// <summary>
        /// This dynamic loader exposes the user interests resource
        /// </summary>
        public UserInterestManager UserInterests
        {
            get
            {
                if (this._userInterestManager != null)
                    return _userInterestManager;
                else
                {
                    this._userInterestManager = new UserInterestManager(this);
                    return _userInterestManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY USERINTERESTS ENDS ------------------------------------------------------------------------------------------------------------------------ //


        // RESOURCES:/ USERINVITERELATIONS ---------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This dynamic loader exposes the User Invite Relations resource
        /// </summary>
        public UserInviteRelationManager UserInviteRelations
        {
            get
            {
                if (this._userInviteRelationManager != null)
                    return _userInviteRelationManager;
                else
                {
                    this._userInviteRelationManager = new UserInviteRelationManager(this);
                    return _userInviteRelationManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY USERS ENDS ------------------------------------------------------------------------------------------------------------------------ //



        // RESOURCES:/ USERS ---------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This dynamic loader exposes the Users resource
        /// </summary>
        public UserManager Users
        {
            get
            {
                if (this._userManager != null)
                    return _userManager;
                else
                {
                    this._userManager = new UserManager(this);
                    return _userManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY USERS ENDS ------------------------------------------------------------------------------------------------------------------------ //



        // RESOURCES:/ VISITOR LOGS ---------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This dynamic loader exposes the Users resource
        /// </summary>
        public VisitorLogManager VisitorLogs
        {
            get
            {
                if (this._visitorLogManager != null)
                    return _visitorLogManager;
                else
                {
                    this._visitorLogManager = new VisitorLogManager(this);
                    return _visitorLogManager;
                } // ELSE ENDS
            } // GET ENDS
        } // PROPERTY  VISITOR LOGS ENDS ------------------------------------------------------------------------------------------------------------------------ //




        // RESOURCES:/ TENANT --------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property exposes the tenat instance which is the parent of this class
        /// </summary>
        public Tenant Tenant => this._tenant;

        // PROPERTY TENANT ENDS ----------------------------------------------------------------------------------------------------------------------- //


        // RESOURCE:/ CONTEXT --------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// This property exposes as accessor property to the instances of the database context object which is necessary for the child managers of this 
        /// class.
        /// </summary>
        public yoyIj7qM58dCjContext Context => this._context;  // PROPERTY CONTEXT ENDS ------------------------------------------------------------------------------------ //

        #endregion

        #region CONSTRUCTORS

        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // CLASS CONSTRUCTORS                                                                                                                             //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tenant"></param>
        protected BusinessObjects(Tenant tenant)
        {
            _context = new yoyIj7qM58dCjContext();
            _tenant = tenant;
        } // CONSTRUCTOR METHOD ENDS -------------------------------------------------------------------------------------------------------------------- //

        #endregion

        #region STATIC_METHODS
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //
        // STATIC CLASS MEHTODS                                                                                                                           //
        // ---------------------------------------------------------------------------------------------------------------------------------------------- //

        /// <summary>
        /// The BusinessObjects can only be instantiated using a factory method that retrieves the correct instance of business objects
        /// </summary>
        /// <param name="tenant"></param>
        /// <returns></returns>
        public static BusinessObjects GetInstance(Tenant tenant)
        {
            return new BusinessObjects(tenant);
        } // METHOD GET INSTANCE ENDS ------------------------------------------------------------------------------------------------------------------- //
        #endregion

    } // CLASS BUSINESS OBJECTS ENDS -------------------------------------------------------------------------------------------------------------------- //
}
