namespace MetadataReader

module Hci =
    let getPacketType byte =
        match byte with
        | 1uy -> "HCI Command Packet"
        | 2uy -> "HCI Asynchronous Data Packet"
        | 4uy -> "HCI Event Packet"
        | unknown -> "Unknown Packet Type: " + unknown.ToString()
    
    module Event =
        let getBleEventCode byte =
            match byte with
            | 0x01uy -> "LE Connection Complete"
            | 0x02uy -> "LE Advertising Report"
            | 0x03uy -> "LE Connection Update Complete"
            | 0x04uy -> "LE Read Remote Used Features Complete"
            | 0x05uy -> "LE Long Term Key Requested"
            | 0x06uy -> "LE Remote Connection Parameter Request"
            | 0x07uy -> "LE Data Length Change"
            | 0x08uy -> "LE Read Local P256 Public Key Complete"
            | 0x09uy -> "LE Generate DHKey Complete"
            | 0x0Auy -> "LE Enhanced Connection Complete"
            | 0x0Buy -> "LE Direct Advertising Report"
            | unknown -> "Unknown LE SubEvent Type: " + unknown.ToString()
        
        let getEventType byte =
            match byte with
            | 0x01uy -> "Inquiry Complete Event"
            | 0x02uy -> "Inquiry Result Event"
            | 0x03uy -> "Connection Complete Event"
            | 0x04uy -> "Connection Request Event"
            | 0x05uy -> "Disconnection Complete Event"
            | 0x06uy -> "Authentication Complete Event"
            | 0x07uy -> "Remote Name Request Complete Event"
            | 0x08uy -> "Encryption Change Event"
            | 0x09uy -> "Change Connection Link Key Complete Event"
            | 0x0Auy -> "Master Link Key Complete Event"
            | 0x0Buy -> "Read Remote Supported Features Complete Event"
            | 0x0Cuy -> "Read Remote Version Complete Event"
            | 0x0Duy -> "QoS Setup Complete Event"
            | 0x0Euy -> "Command Complete Event"
            | 0x0Fuy -> "Command Status Event"
            | 0x10uy -> "Hardware Error Event"
            | 0x11uy -> "Flush Occured Event"
            | 0x12uy -> "Role Change Event"
            | 0x13uy -> "Number Of Completed Packets Event"
            | 0x14uy -> "Mode Change Event"
            | 0x15uy -> "Return Link Keys Event"
            | 0x16uy -> "PIN Code Request Event"
            | 0x17uy -> "Link Key Request Event"
            | 0x18uy -> "Link Key Notification Event"
            | 0x19uy -> "Loopback Command Event"
            | 0x1Auy -> "Data Buffer Overflow Event"
            | 0x1Buy -> "Max Slots Change Event"
            | 0x1Cuy -> "Read Clock Offset Complete Event"
            | 0x1Duy -> "Connection Packet Type Changed Event"
            | 0x1Euy -> "QoS Violation Event"
            | 0x1Fuy -> "Page Scan Mode Change Event"
            | 0x20uy -> "Page Scan Repetition Mode Change Event"
            | 0x3Euy -> "LE Meta"
            | unknown -> "Unknown Event Type: " + unknown.ToString()
    
    module Command =
        let getOpcodeGroupFieldKey (byte : byte) = byte >>> 2
        let getOpcodeCommandFieldKey (bytes : byte []) = (((bytes.[1] &&& 0b00000011uy) <<< 8) ||| bytes.[0])
        
        let getOpcodeCommandField ogf ocf =
            match ogf with
            | 0x01uy -> 
                match ocf with
                | 0x0001uy -> "Inquiry"
                | 0x0002uy -> "Inquiry Cancel"
                | 0x0003uy -> "Periodic Inquiry Mode"
                | 0x0004uy -> "Exit Periodic Inquiry Mode"
                | 0x0005uy -> "Create Connection"
                | 0x0006uy -> "Disconnect"
                | 0x0007uy -> "Add SCO Connection"
                | 0x0009uy -> "Accept Connection Request"
                | 0x000Auy -> "Reject Connection Request"
                | 0x000Buy -> "Link Key Request Reply"
                | 0x000Cuy -> "Link Key Request Negative Reply"
                | 0x000Duy -> "PIN Code Request Reply"
                | 0x000Euy -> "PIN Code Request Negative Reply"
                | 0x000Fuy -> "Change Connection Packet Type"
                | 0x0011uy -> "Authentication Requested"
                | 0x0013uy -> "Set Connection Encryption"
                | 0x0015uy -> "Change Connection Link Key"
                | 0x0017uy -> "Master Link Key"
                | 0x0019uy -> "Remote Name Request"
                | 0x001Buy -> "Read Remote Supported Features"
                | 0x001Duy -> "Read Remote Version Information"
                | 0x001Fuy -> "Read Clock Offset"
                | unknown -> "Unknown OCF (OGF: Link Control Command): " + unknown.ToString()
            | 0x02uy -> 
                match ocf with
                | 0x0001uy -> "Hold Mode"
                | 0x0003uy -> "Sniff Mode"
                | 0x0004uy -> "Exit Sniff Mode"
                | 0x0005uy -> "Park Mode"
                | 0x0006uy -> "Exit Park Mode"
                | 0x0007uy -> "QoS Setup"
                | 0x0009uy -> "Role Discovery"
                | 0x000Buy -> "Switch Role"
                | 0x000Cuy -> "Read Link Policy Settings"
                | 0x000Duy -> "Write Link Policy Settings"
                | unknown -> "Unknown OCF (OGF: HCI Policy Command): " + unknown.ToString()
            | 0x03uy -> 
                match ocf with
                | 0x0001uy -> "Set Event Mask"
                | 0x0003uy -> "Reset"
                | 0x0005uy -> "Set Event Filter"
                | 0x0008uy -> "Flush"
                | 0x0009uy -> "Read PIN Type"
                | 0x000Auy -> "Write PIN Type"
                | 0x000Buy -> "Create New Unit Key"
                | 0x000Duy -> "Read Stored Link Key"
                | 0x0011uy -> "Write Stored Link Key"
                | 0x0012uy -> "Delete Stored Link Key"
                | 0x0013uy -> "Change Local Name"
                | 0x0014uy -> "Read Local Name"
                | 0x0015uy -> "Read Connection Accept Timeout"
                | 0x0016uy -> "Write Connection Accept Timeout"
                | 0x0017uy -> "Read Page Timeout"
                | 0x0018uy -> "Write Page Timeout"
                | 0x0019uy -> "Read Scan Enable"
                | 0x001Auy -> "Write Scan Enable"
                | 0x001Buy -> "Read Page Scan Activity"
                | 0x001Cuy -> "Write Page Scan Activity"
                | 0x001Duy -> "Read Inquiry Scan Activity"
                | 0x001Euy -> "Write Inquiry Scan Activity"
                | 0x001Fuy -> "Read Authentication Enable"
                | 0x0020uy -> "Write Authentication Enable"
                | 0x0021uy -> "Read Encryption Mode"
                | 0x0022uy -> "Write Encryption Mode"
                | 0x0023uy -> "Read Class Of Device"
                | 0x0024uy -> "Write Class Of Device"
                | 0x0025uy -> "Read Voice Setting"
                | 0x0026uy -> "Write Voice Setting"
                | 0x0027uy -> "Read Automatic Flush Timeout"
                | 0x0028uy -> "Write Automatic Flush Timeout"
                | 0x0029uy -> "Read Num Broadcast Retransmissions"
                | 0x002Auy -> "Write Num Broadcast Retransmissions"
                | 0x002Buy -> "Read Hold Mode Activity"
                | 0x002Cuy -> "Write Hold Mode Activity"
                | 0x002Duy -> "Read Transmit Power Level"
                | 0x002Euy -> "Read SCO Flow Control Enable"
                | 0x002Fuy -> "Write SCO Flow Control Enable"
                | 0x0031uy -> "Set Host Controller To Host Flow Control"
                | 0x0033uy -> "Host Buffer Size"
                | 0x0035uy -> "Host Number Of Completed Packets"
                | 0x0036uy -> "Read Link Supervision Timeout"
                | 0x0037uy -> "Write Link Supervision Timeout"
                | 0x0038uy -> "Read Number Of Supported IAC"
                | 0x0039uy -> "Read Current IAC LAP"
                | 0x003Auy -> "Write Current IAC LAP"
                | 0x003Buy -> "Read Page Scan Period Mode"
                | 0x003Cuy -> "Write Page Scan Period Mode"
                | 0x003Duy -> "Read Page Scan Mode"
                | 0x003Euy -> "Write Page Scan Mode"
                | unknown -> "Unknown OCF (OGF: Host Controller And Baseband Command): " + unknown.ToString()
            | unknown -> "Unknown (OGF, OCF): (" + unknown.ToString() + ", " + ocf.ToString() + ")"
    
    module AsynchronousData =
        let getHandle (bytes : byte []) = (((bytes.[1] &&& 0b00001111uy) <<< 8) ||| bytes.[0])
        let getPbFlag (byte : byte) = (byte >>> 4) &&& 0b11uy
        let getBcFlag (byte : byte) = byte >>> 6
        let getDataTotalLength (bytes : byte []) = (bytes.[1] <<< 8) ||| bytes.[0]