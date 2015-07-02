#region

using System;

#endregion

namespace CanonGeneric
{
	public class Bjnp
	{
		#region Static Fields

		public const int BJNP_PRINTBUF_MAX = 4096; /* size of printbuffer */
		public const int BJNP_CMD_MAX = 2048; /* size of BJNP response buffer */
		public const int BJNP_RESP_MAX = 2048; /* size of BJNP response buffer */
		public const int BJNP_SOCK_MAX = 256; /* maximum number of open sockets */
		public const int BJNP_MODEL_MAX = 64; /* max allowed size for make&model */
		public const int BJNP_IEEE1284_MAX = 1024; /* max. allowed size of IEEE1284 id */
		public const int KEEP_ALIVE_SECONDS = 3; /* max interval/2 seconds before we */
		/* send an empty data packet to the */
		/* printer */
		public const int HOSTNAME_MAX = 128;

		#endregion

		#region Private Fields

		public bjnp_cmd_e bjnp_cmd_t;
		public uint8_t bjnp_cmd_type_t;
		public bjnp_loglevel_e bjnp_loglevel_t;
		public bjnp_paper_status_e bjnp_paper_status_t;
		public bjnp_port_e bjnp_port_t;

		#endregion
	}

/* port numbers */

	public enum bjnp_port_e
	{
		BJNP_PORT_PRINT = 8611,
		BJNP_PORT_SCAN = 8612,
		BJNP_PORT_3 = 8613,
		BJNP_PORT_4 = 8614
	};

//#define BJNP_STRING "BJNP"

/* commands */

	public enum bjnp_cmd_e
	{
		CMD_UDP_DISCOVER = 0x01, /* discover if service type is listening at this port */
		CMD_UDP_PRINT_JOB_DET = 0x10, /* send print job owner details */
		CMD_UDP_CLOSE = 0x11, /* request connection closure */
		CMD_TCP_PRINT = 0x21, /* print */
		CMD_UDP_GET_STATUS = 0x20, /* get printer status  */
		CMD_UDP_GET_ID = 0x30, /* get printer identity */
		CMD_UDP_SCAN_JOB = 0x32 /* send scan job owner details */
	};

/* command type */

	public enum uint8_t
	{
		BJNP_CMD_PRINT = 0x1, /* printer command */
		BJNP_CMD_SCAN = 0x2, /* scanner command */
		BJNP_RES_PRINT = 0x81, /* printer response */
		BJNP_RES_SCAN = 0x82 /* scanner response */
	};


	public unsafe struct BJNP_command
	{
		#region Private Fields

		public fixed char BJNP_id [4]; /* string: BJNP */
		public uint8_t cmd_code; /* command code/response code */
		public uint8_t dev_type; /* 1 = printer, 2 = scanner */
		public Int32 payload_len; /* length of command buffer */
		public Int32 seq_no; /* sequence number */
		public Int16 session_id; /* session id for printing */

		#endregion
	};

/* Layout of the init response buffer */

	public unsafe struct INIT_RESPONSE
	{
		#region Private Fields

		public fixed int ip_addr [4]; /* printers IP-address */
		public fixed char mac_addr [6]; /* printers mac address */
		public BJNP_command response; /* reponse header */
		public fixed char unknown1 [6]; /* 00 01 08 00 06 04 */

		#endregion
	};

/* layout of payload for the JOB_DETAILS command */

	public unsafe struct JOB_DETAILS
	{
		#region Private Fields

		public BJNP_command cmd; /* command header */
		public fixed char hostname [64]; /* hostname of sender */
		public fixed char jobtitle [256]; /* job title */
		public fixed char unknown [8]; /* don't know what these are for */
		public fixed char username [64]; /* username */

		#endregion
	};

/* Layout of ID and status responses */

	public unsafe struct IDENTITY
	{
		#region Private Fields

		public BJNP_command cmd;
		public fixed char id [2048]; /* identity */
		public UInt16 id_len; /* length of identity */

		#endregion
	};


/* response to TCP print command */

	public struct PRINT_RESP
	{
		#region Private Fields

		private BJNP_command cmd;
		private UInt32 num_printed; /* number of print bytes received */

		#endregion
	};

	public enum bjnp_paper_status_e
	{
		BJNP_PAPER_UNKNOWN = -1,
		BJNP_PAPER_OK = 0,
		BJNP_PAPER_OUT = 1
	};


/* 
 *  BJNP definitions 
 */


/*
 * structure that stores information on found printers
 */

	public struct in_addr
	{
		private ulong s_addr; // load with inet_aton()
	};

	public unsafe struct sockaddr_in
	{
		public short sin_family; // e.g. AF_INET
		public ushort sin_port; // e.g. htons(3490)
		public in_addr sin_addr; // see struct in_addr, below
		public fixed char sin_zero [8]; // zero this if you want to
	};

	public unsafe struct printer_list
	{
		#region Private Fields

		public sockaddr_in addr; /* address/port of printer */
		public fixed char hostname [256]; /* hostame, if found, else ip-address */
		public fixed char ip_address [16];
		public fixed char model [Bjnp.BJNP_MODEL_MAX]; /* printer make and model */
		private int port; /* udp/tcp port */

		#endregion
	};

	public enum bjnp_loglevel_e
	{
		LOG_NONE,
		LOG_EMERG,
		LOG_ALERT,
		LOG_CRIT,
		LOG_ERROR,
		LOG_WARN,
		LOG_NOTICE,
		LOG_INFO,
		LOG_DEBUG,
		LOG_DEBUG2,
		LOG_END /* not a real loglevel, but indicates end */
		/* of list */
	};

}