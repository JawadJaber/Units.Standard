



#define USE_DECIMAL // <== Toggle this to enable decimal or disable for double

#if USE_DECIMAL
using Real = System.Decimal;
#else
using Real = System.Double;
#endif



namespace Units.Standard
{

}
