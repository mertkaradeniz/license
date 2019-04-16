library JMT;

{ Important note about DLL memory management: ShareMem must be the
  first unit in your library's USES clause AND your project's (select
  Project-View Source) USES clause if your DLL exports any procedures or
  functions that pass strings as parameters or function results. This
  applies to all strings passed to and from your DLL--even those that
  are nested in records and classes. ShareMem is the interface unit to
  the BORLNDMM.DLL shared memory manager, which must be deployed along
  with your DLL. To avoid using BORLNDMM.DLL, pass string information
  using PChar or ShortString parameters. }
uses
  SysUtils,
  NB30,
  math,
  dialogs,
  Windows,
  Messages,
  Variants,
  Graphics,
  Controls,
  DateUtils,
  Registry,
  Classes;
{$R *.res}

  function LiShowParaMetreMAC(MAC:Int64):Int64;  stdcall;
 begin
 Randomize;
       Result :=(mac*(Random(20)+11)+61);
 end;

 function LiShowParaMetreVol(Volume:Int64):Int64;  stdcall;
 begin
      Randomize;
       Result :=volume*(Random(31)+11)+61;
 end;

 function LiDemoPathKey():double;stdcall;
 begin
   result :=02071996211001613684;
 end;

      function BackCount():integer; stdcall;
      var
 Reg1:TRegistry;
    DateReg:integer;
 begin
     Reg1:=TRegistry.Create;
  Reg1.RootKey:=HKEY_LOCAL_MACHINE;
  Reg1.OpenKey('SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Installer\\UserData\\S-1-5-18\\Components\\F5674109110043561000E0239E6F5E85',True);
  DateReg:= Reg1.ReadInteger('F5674109110043561000E0239E6F5E85');
  Reg1.CloseKey;
Reg1.Free;
result :=DateReg ;
   end;

     function LiFinishRead():bool; stdcall;
      var
 Reg1:TRegistry;
    DateReg:integer;
 begin
     Reg1:=TRegistry.Create;
  Reg1.RootKey:=HKEY_LOCAL_MACHINE;
  Reg1.OpenKey('SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Installer\\UserData\\S-1-5-18\\Components\\C56741578112010043561000E0239E6F5E85',True);
  DateReg:= Reg1.ReadInteger('C56741578112010043561000E0239E6F5E85');
  Reg1.CloseKey;
Reg1.Free;
     if (DateReg) = 6100001  then
  result:=true
     else if (DateReg) = 5400001  then
     result:=false;
         end;

 function LiDateReg(Date:integer ):bool;  stdcall;
  var
    Reg1:TRegistry;
    DateReg:integer;
  begin

  DateReg:=Date;
       Reg1:=TRegistry.Create;
  Reg1.RootKey:=HKEY_LOCAL_MACHINE;
  Reg1.OpenKey('SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Installer\\UserData\\S-1-5-18\\Components\\F5674109110043561000E0239E6F5E85',True);
  Reg1.WriteInteger('F5674109110043561000E0239E6F5E85',DateReg);
  Reg1.CloseKey;
Reg1.Free;
result :=true;
  end;

    function LiDemoStart():bool;  stdcall;
  var
    Reg1:TRegistry;
  begin
       Reg1:=TRegistry.Create;
  Reg1.RootKey:=HKEY_LOCAL_MACHINE;
  Reg1.OpenKey('SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Installer\\UserData\\S-1-5-18\\Components\\C56741578112010043561000E0239E6F5E85',True);
  Reg1.WriteInteger('C56741578112010043561000E0239E6F5E85',5400001);
  Reg1.CloseKey;
Reg1.Free;
result :=true;
  end;

   function LiDemoFinish():bool;  stdcall;
  var
    Reg1:TRegistry;
  begin
       Reg1:=TRegistry.Create;
  Reg1.RootKey:=HKEY_LOCAL_MACHINE;
  Reg1.OpenKey('SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Installer\\UserData\\S-1-5-18\\Components\\C56741578112010043561000E0239E6F5E85',True);
  Reg1.WriteInteger('C56741578112010043561000E0239E6F5E85',6100001);
  Reg1.CloseKey;
Reg1.Free;
result :=true;
  end;

   function LiParametreSeriN(gelen_pr:int64 ):double  ;  stdcall;
  var
  kalan :Array [0..20] of string;
  SON_SONUC :string ;
  bolum,SONUC :string ;
  i,i2,ona,sy : integer;
  gelen_p,t_cevir:int64 ;
  Reg1:TRegistry;
  begin

  gelen_p := gelen_pr;
  i:=0;
  While gelen_p >=16 do
  begin
  kalan[i] := intToStr( gelen_p mod 16);
  gelen_p := gelen_p div 16 ;
  i:=i+1 ;
  end;

  gelen_p := (gelen_p + 1) * 2;

  For sy:=0 To i do
  begin
  if Trim(kalan[sy]) <> ''  then
  begin
  kalan[sy] := inttostr(strtoint(kalan[sy]) + 1);
  end;
  end;

  For ona:=0 To i do
  begin
  SONUC := SONUC +kalan[ona];
  end;

  SONUC :=SONUC+inttostr(gelen_p);
  t_cevir := strtoint64(SONUC);
  i2:=0;

  While t_cevir >=16 do
  begin
  kalan[i2] := inttostr(t_cevir mod 16);
  t_cevir :=(t_cevir div 16) ;
  i2:=i2+1;
  end;

  bolum := inttostr(gelen_p);

  if (bolum = '10') then
   bolum := 'A';

  if (bolum = '11')  then
   bolum := 'B';

  if (bolum = '12')  then
   bolum := 'C';

  if (bolum = '13')  then
   bolum := 'D';

  if (bolum = '14')  then
   bolum := 'E';

  if (bolum = '15')   then
   bolum := 'F';

   For ona:=0 To i do
   begin

   if (kalan[ona] = '10') then
   kalan[ona] := 'A';

   if (kalan[ona] = '11')  then
   kalan[ona] := 'B';

   if (kalan[ona] = '12')  then
   kalan[ona] := 'C';

   if (kalan[ona] = '13')  then
   kalan[ona] := 'D';

   if (kalan[ona] = '14')  then
   kalan[ona] := 'E';

   if (kalan[ona] = '15')   then
   kalan[ona] := 'F';
   end;


   For ona:=0 To i-1 do
   begin

   SON_SONUC := SON_SONUC+kalan[ona];
   end;

   SON_SONUC :=  SON_SONUC+bolum ;


 Reg1:=TRegistry.Create;
  Reg1.RootKey:=HKEY_LOCAL_MACHINE;
  Reg1.OpenKey('Software\LiParametre\Parametre',True);
  Reg1.WriteString('LiParametreSeriNumber',SON_SONUC);
  Reg1.CloseKey;
Reg1.Free;

     result:=0;
  end;

  function LiParametreDateReplaceKey(gelen_pr:int64 ):double  ; stdcall;
  var
  kalan :Array [0..20] of string;
  SON_SONUC :string ;
  bolum,SONUC :string ;
  i,i2,ona,sy : integer;
  gelen_p,t_cevir:int64 ;
  Reg1:TRegistry;
  begin

  gelen_p := gelen_pr;
  i:=0;
  While gelen_p >=16 do
  begin
  kalan[i] := intToStr( gelen_p mod 16);
  gelen_p := gelen_p div 16 ;
  i:=i+1 ;
  end;

  gelen_p := (gelen_p + 1) * 2;

  For sy:=0 To i do
  begin
  if Trim(kalan[sy]) <> ''  then
  begin
  kalan[sy] := inttostr(strtoint(kalan[sy]) + 1);
  end;
  end;

  For ona:=0 To i do
  begin
  SONUC := SONUC +kalan[ona];
  end;

  SONUC :=SONUC+inttostr(gelen_p);
  t_cevir := strtoint64(SONUC);
  i2:=0;

  While t_cevir >=16 do
  begin
  kalan[i2] := inttostr(t_cevir mod 16);
  t_cevir :=(t_cevir div 16) ;
  i2:=i2+1;
  end;

  bolum := inttostr(gelen_p);

  if (bolum = '10') then
   bolum := 'A';

  if (bolum = '11')  then
   bolum := 'B';

  if (bolum = '12')  then
   bolum := 'C';

  if (bolum = '13')  then
   bolum := 'D';

  if (bolum = '14')  then
   bolum := 'E';

  if (bolum = '15')   then
   bolum := 'F';

   For ona:=0 To i do
   begin

   if (kalan[ona] = '10') then
   kalan[ona] := 'A';

   if (kalan[ona] = '11')  then
   kalan[ona] := 'B';

   if (kalan[ona] = '12')  then
   kalan[ona] := 'C';

   if (kalan[ona] = '13')  then
   kalan[ona] := 'D';

   if (kalan[ona] = '14')  then
   kalan[ona] := 'E';

   if (kalan[ona] = '15')   then
   kalan[ona] := 'F';
   end;


   For ona:=0 To i-1 do
   begin

   SON_SONUC := SON_SONUC+kalan[ona];
   end;

   SON_SONUC :=  SON_SONUC+bolum ;


 Reg1:=TRegistry.Create;
  Reg1.RootKey:=HKEY_LOCAL_MACHINE;
  Reg1.OpenKey('Software\LiParametre\Parametre',True);
  Reg1.WriteString('LiParametreReplaceKey',SON_SONUC);
  Reg1.CloseKey;
Reg1.Free;
     result:=0;
  end;

  exports
  LiDemoPathKey,LiDemoStart,LiFinishRead,LiDemoFinish,LiParametreSeriN,LiParametreDateReplaceKey,LiShowParaMetreMAC,LiShowParaMetreVol,LiDateReg,BackCount;
  begin
end.

